import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type User, type InputCreateUser, type AccountDto, type GenerateUserAccount } from '@/models/Users/user';
import { type PaginationParams } from '@/models/paginationParams';

export const useUserStore = defineStore('usersStore', () => {
  const users = ref<User[]>([]);
  const totalItems = ref(0);
  const accountInfo = ref<AccountDto | null>(null);

  function addNewUser(user: User) {
    users.value.push(user);
  }

  function removeUser(userId: string): boolean {
    const idx = users.value.findIndex((s) => s.userId === userId);
    if (idx === -1) return false;
    users.value.splice(idx, 1);
    return true;
  }

  function updateUser(updatedUser: User): boolean {
    const existingUserIndex = users.value.findIndex((m) => m.userId === updatedUser.userId);

    if (existingUserIndex !== -1) {
      users.value[existingUserIndex] = updatedUser;
      return true;
    }
    return false;
  }

  async function dispatchGetUsers(pagination: PaginationParams) {
    const { data } = await API.users.getPaginatedUsers(pagination);
    users.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateUser(input: InputCreateUser) {
    const { data } = await API.users.signUp(input);
    addNewUser(data);
  }

  async function dispatchDeleteUser(userId: string) {
    await API.users.deleteUser(userId);
    removeUser(userId);
  }

  async function dispatchGetUser(userId: string) {
    const { data } = await API.users.getAccountInfo(userId);
    return data;
  }

  async function dispatchGetAccountInfo() {
    const { data } = await API.users.myAccount();
    accountInfo.value = data;
  }

  async function dispatchGenerateUserAccount(command: GenerateUserAccount) {
    const { data } = await API.users.generateUserAccount(command);
    addNewUser(data);
  }

  return {
    users,
    totalItems,
    accountInfo,
    dispatchCreateUser,
    dispatchGetUsers,
    dispatchDeleteUser,
    dispatchGetUser,
    dispatchGetAccountInfo,
    dispatchGenerateUserAccount
  };
});