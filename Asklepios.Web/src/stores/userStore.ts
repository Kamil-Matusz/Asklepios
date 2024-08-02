import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type User, type InputCreateUser, type AccountDto, type GenerateUserAccount, UserAutocompleteDto } from '@/models/Users/user';
import { type PaginationParams } from '@/models/paginationParams';
import { da } from 'vuetify/locale';

export const useUserStore = defineStore('usersStore', () => {
  const users = ref<User[]>([]);
  const totalItems = ref(0);
  const accountInfo = ref<AccountDto | null>(null);
  const currentUser = ref<User | null>(null);
  const nurses = ref<UserAutocompleteDto[]>([]);
  const doctors = ref<UserAutocompleteDto[]>([]);

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

  async function fetchCurrentUser() {
    try {
      const { data } = await API.users.myAccount();
      currentUser.value = data;
    } catch (error) {
      console.error('Error fetching current user:', error);
    }
  }

  async function dispatchChangeUserRole(userId: string, role: string) {
    await API.users.changeUserRole(userId, role);
    const updatedUser = users.value.find((user) => user.userId === userId);
    if (updatedUser) {
      updatedUser.role = role;
    }
  }

  async function dispatchChangeAccountStatus(userId: string, status: boolean) {
    await API.users.changeAccountStatus(userId, status);
    const user = users.value.find((u) => u.userId === userId);
    if (user) {
      user.isActive = status;
    }
  }

  async function dispatchGetNurses() {
    const { data } = await API.users.getNursesList();
    nurses.value = data;
    return data as UserAutocompleteDto[];
  }

  async function dispatchGetDoctors() {
    const { data } = await API.users.getDoctorsList();
    doctors.value = data;
    return data as UserAutocompleteDto[];
  }

  return {
    users,
    totalItems,
    accountInfo,
    currentUser,
    nurses,
    doctors,
    dispatchCreateUser,
    dispatchGetUsers,
    dispatchDeleteUser,
    dispatchGetUser,
    dispatchGetAccountInfo,
    dispatchGenerateUserAccount,
    fetchCurrentUser,
    dispatchChangeUserRole,
    dispatchChangeAccountStatus,
    dispatchGetNurses,
    dispatchGetDoctors
  };
});
