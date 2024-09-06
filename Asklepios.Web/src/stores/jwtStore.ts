import { defineStore } from "pinia";
import { ref } from "vue";
import { API } from "../services/index";
import { InputLoginData, JwtToken } from "@/models/authorization";
import { useToast } from "vue-toastification";
import { User } from "@/models/Users/user";
import router from "@/router";
import * as signalR from '@microsoft/signalr';

const toast = useToast();

export const useJwtStore = defineStore("JwtStore", () => {
  const token = ref<JwtToken>({ jwt: "" });
  const isLoggedIn = ref(false);
  const connection = ref<signalR.HubConnection | null>(null);

  async function dispatchLogin(loginData: InputLoginData) {
    try {
      const { data } = await API.jwt.login(loginData);

      // Ustawienie tokenu JWT
      token.value.jwt = data.accessToken;
      localStorage.setItem("jwtToken", data.accessToken);

      // Dekodowanie tokenu JWT
      const base64Url = token.value.jwt.split(".")[1];
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split("")
          .map(function (c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );
      const payload = JSON.parse(jsonPayload);

      const user: User = {
        userId: payload.sub,
        email: payload.unique_name,
        role: payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        isActive: true,
        createdAt: new Date(),
      };
      localStorage.setItem("user", JSON.stringify(user));

      initSignalR(user.userId);

      isLoggedIn.value = true;
      toast.success("Zalogowano pomyślnie!");
      router.push("/dashboard");
    } catch (error: any) {
      console.error("Login error:", error);
      if (error.response && error.response.status === 403) {
        toast.error("Nieprawidłowy adres e-mail lub hasło. Spróbuj ponownie.");
      } else {
        toast.error("Wystąpił błąd podczas logowania. Spróbuj ponownie później.");
      }
    }
  }

  function initSignalR(userId: string) {
    connection.value = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5102/hospitalHub")
      .build();

      connection.value.start()
      .then(() => {
        console.log("SignalR Connected");
        if (connection.value) {
          connection.value.invoke("JoinDoctorGroup", userId);
        }
      })
      .catch(err => console.error("SignalR Connection Error: ", err));

      connection.value.on("ReceiveNotification", (message) => {
        toast.info(`Powiadomienie: ${message}`);
      });
  }

  function dispatchLogout() {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("user");
    token.value.jwt = "";
    isLoggedIn.value = false;

    if (connection.value) {
      connection.value.stop();
      connection.value = null;
    }

    toast.success("Wylogowano pomyślnie.");
    router.push("/");
  }

  function getUserRole(): string | null {
    const user = localStorage.getItem("user");
    if (user) {
      const userData: User = JSON.parse(user);
      return userData.role;
    }
    return null;
  }

  function getUser(): User | null {
    const user = localStorage.getItem("user");
    if (user) {
      const userData: User = JSON.parse(user);
      return userData;
    }
    return null;
  }

  function isJwtTokenExists(): boolean {
    return localStorage.getItem("jwtToken") !== null;
  }

  return {
    dispatchLogin,
    dispatchLogout,
    isJwtTokenExists,
    token,
    getUserRole,
    getUser,
  };
});
