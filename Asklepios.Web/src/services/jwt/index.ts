import httpClient from "../httpClient";
import { type JwtToken, InputJwtToken, InputLoginData } from "@/models/authorization";

const base = 'users-module/Users/signIn';

async function login(input: InputLoginData) {
  return await httpClient.post<JwtToken>(base, input);
}

export default {
  login
};
