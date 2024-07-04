import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type User, type InputCreateUser, type AccountDto, type GenerateUserAccount } from '@/models/Users/user';

const base = 'users-module/Users';

async function getPaginatedUsers(pagination: PaginationParams) {
  return await httpClient.get<User[]>(base, { params: pagination });
}

async function deleteUser(userId: string) {
  return await httpClient.delete<boolean>(`${base}/${userId}`);
}

async function signUp(input: InputCreateUser) {
  return await httpClient.post<AccountDto>(`${base}/signUp`, input);
}

async function getAccountInfo(userId: string) {
  return await httpClient.get<AccountDto>(`${base}/${userId}`);
}

async function myAccount() {
  return await httpClient.get<AccountDto>(`${base}/myAccount`);
}
async function generateUserAccount(command: GenerateUserAccount) {
  return await httpClient.post<User>(`${base}/generateUserAccount`, command);
}


export default {
  getPaginatedUsers,
  deleteUser,
  signUp,
  getAccountInfo,
  myAccount,
  generateUserAccount
};
