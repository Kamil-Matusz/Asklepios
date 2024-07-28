import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type User, type InputCreateUser, type AccountDto, type GenerateUserAccount, UserAutocompleteDto } from '@/models/Users/user';

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
  try {
    console.log('Sending command:', JSON.stringify(command));
    return await httpClient.post<User>(`${base}/generateUserAccount`, command, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
  } catch (error) {
    console.error('Error generating user account:', error);
    throw error;
  }
}

async function changeUserRole(userId: string, role: string) {
  try {
    return await httpClient.put<void>(`${base}/${userId}/changeUserRole`, { role }, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
  } catch (error) {
    console.error('Error changing user role:', error);
    throw error;
  }
}

async function changeAccountStatus(userId: string, status: boolean) {
  return await httpClient.put<boolean>(`${base}/${userId}/changeAccountStatus`, { status });
}

async function getNursesList() {
  return await httpClient.get<UserAutocompleteDto[]>(`${base}/nursesList`);
}

export default {
  getPaginatedUsers,
  deleteUser,
  signUp,
  getAccountInfo,
  myAccount,
  generateUserAccount,
  changeUserRole,
  changeAccountStatus,
  getNursesList
};
