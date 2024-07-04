import { defineStore } from "pinia";
import { ref } from "vue";
import { API } from "../services/index";
import { InputLoginData, JwtToken } from "@/models/authorization"
import { useToast } from "vue-toastification";
import { User } from "@/models/Users/user";
import router from "@/router";

const toast = useToast();

export const useJwtStore = defineStore("JwtStore", () => {
  const token = ref<JwtToken>({ jwt: "" });
  const isLoggedIn = ref(false);

  async function dispatchLogin(loginData: InputLoginData) {
    try {
      const { data } = await API.jwt.login(loginData);
      token.value.jwt = data.jwt;
      localStorage.setItem("jwtToken", data.jwt);
      toast.success("Zalogowano pomyślnie!");
      isLoggedIn.value = true;
      router.push("/profile");

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
        userId: payload.id,
        email: payload.email,
        role: payload.role,
        isActive: true,
        createdAt: new Date(),
      };

      localStorage.setItem("user", JSON.stringify(user));

      toast.success("Zalogowano pomyślnie!");
    } catch (error: any) {
      if (error.response && error.response.status === 403) {
        toast.error(
          "Nieprawidłowy adres e-mail lub hasło. Spróbuj ponownie."
        );
      } else {
        toast.error(
          "Wystąpił błąd podczas logowania. Spróbuj ponownie później."
        );
      }
    }
  }

  function dispatchLogout() {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("user");
    token.value.jwt = "";
    isLoggedIn.value = false;

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
