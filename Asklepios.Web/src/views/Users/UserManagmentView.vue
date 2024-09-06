<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { useToast } from 'vue-toastification';
import { GenerateUserAccount } from '@/models/Users/user';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import GenerateUserForm from '@/components/users/GenerateUserForm.vue';
import ChangeUserRoleForm from '@/components/users/ChangeUserRoleForm.vue';
import ChangeUserStatusForm from '@/components/users/ChangeAccountStatusForm.vue';

const usersStore = useUserStore();
const toast = useToast();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Email', key: 'email', align: 'start' },
  { title: 'Rola', key: 'role', align: 'start' },
  { title: 'Aktywne', key: 'isActive', align: 'start' },
  { title: 'Utworzone', key: 'createdAt', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const userToAdd = ref<GenerateUserAccount>({
  email: '',
  role: '',
  isActive: true
});

const userToEdit = ref<GenerateUserAccount | null>(null);
const isEditDialogActive = ref(false);

const userToChangeRole = ref<GenerateUserAccount | null>(null);
const isRoleDialogActive = ref(false);

const userToChangeStatus = ref<{ userId: string, isActive: boolean } | null>(null);
const isStatusDialogActive = ref(false);

const translateRole = (role: string) => {
  const roleTranslations: { [key: string]: string } = {
    'Admin': 'Administrator',
    'Doctor': 'Doktor',
    'Nurse': 'Pielęgniarka',
    'IT Employee': 'Pracownik IT',
    'Patient': 'Pacjent'
  };
  return roleTranslations[role] || role;
};

const getUsers = async () => {
  options.value.loading = true;
  try {
    await usersStore.dispatchGetUsers({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = usersStore.totalItems;
  } catch (error) {
    console.error('Error fetching users:', error);
    toast.error('Wystąpił problem podczas pobierania użytkowników');
  } finally {
    options.value.loading = false;
  }
};

const addUser = async (user: GenerateUserAccount) => {
  try {
    if (!user.email || !user.role) {
      console.error('Wymagane są wszystkie pola: Email i Rola');
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    console.log('Adding user with data:', user);
    await usersStore.dispatchGenerateUserAccount(user);
    toast.success('Pomyślnie dodano nowego użytkownika!');
    userToAdd.value = { email: '', role: '', isActive: true };
    getUsers();
  } catch (error) {
    console.error('Error adding user:', error);
    toast.error('Wystąpił problem podczas dodawania użytkownika');
  }
};

const deleteUser = async (id: string) => {
  await usersStore.dispatchDeleteUser(id);
  toast.success('Pomyślnie usunięto użytkownika!');
  getUsers();
};

const updateUser = async () => {
  if (userToEdit.value) {
    await usersStore.updateUser(userToEdit.value);
    toast.success('Pomyślnie zaktualizowano dane użytkownika!');
    getUsers();
  }
};

const openEditDialog = (user: GenerateUserAccount) => {
  userToEdit.value = { ...user };
  isEditDialogActive.value = true;
};

const openRoleDialog = (user: GenerateUserAccount) => {
  userToChangeRole.value = { ...user };
  isRoleDialogActive.value = true;
};

const openStatusDialog = (user: GenerateUserAccount) => {
  userToChangeStatus.value = { userId: user.userId, isActive: user.isActive };
  isStatusDialogActive.value = true;
};

const handleChangeUserRole = async (updatedUser: { userId: string, role: string }) => {
  try {
    await usersStore.dispatchChangeUserRole(updatedUser.userId, updatedUser.role);
    toast.success('Pomyślnie zaktualizowano rolę użytkownika!');
    isRoleDialogActive.value = false;
    getUsers();
  } catch (error) {
    console.error('Error changing user role:', error);
    toast.error('Wystąpił problem podczas aktualizacji roli użytkownika');
  }
};

const handleChangeUserStatus = async (updatedUser: { userId: string, isActive: boolean }) => {
  try {
    await usersStore.dispatchChangeAccountStatus(updatedUser.userId, updatedUser.isActive);
    toast.success('Pomyślnie zaktualizowano status użytkownika!');
    isStatusDialogActive.value = false;
    getUsers();
  } catch (error) {
    console.error('Error changing user status:', error);
    toast.error('Wystąpił problem podczas aktualizacji statusu użytkownika');
  }
};

const handleInvalidSubmit = () => {
  toast.error('Formularz zawiera błędy. Proszę poprawić i spróbować ponownie.');
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getUsers();
};

onMounted(getUsers);
</script>

<template>
  <BasePage title="Zarządzanie użytkownikami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem"
          >+Dodaj nowego użytkownika</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowy użytkownik" rounded="lg">
            <GenerateUserForm
              v-model="userToAdd"
              @on-valid-submit="(user) => { addUser(user); isActive.value = false; }"
            ></GenerateUserForm>
          </v-card>
        </template>
      </v-dialog>

      <v-dialog v-model="isRoleDialogActive" max-width="500">
        <template #default>
          <v-card title="Edytuj rolę użytkownika" rounded="lg">
            <ChangeUserRoleForm
              :user="userToChangeRole"
              @onValidSubmit="handleChangeUserRole"
              @onInvalidSubmit="handleInvalidSubmit"
            ></ChangeUserRoleForm>
          </v-card>
        </template>
      </v-dialog>

      <v-dialog v-model="isStatusDialogActive" max-width="500">
        <template #default>
          <v-card title="Edytuj status użytkownika" rounded="lg">
            <ChangeUserStatusForm
              :user="userToChangeStatus"
              @onValidSubmit="handleChangeUserStatus"
              @onInvalidSubmit="handleInvalidSubmit"
            ></ChangeUserStatusForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="usersStore.users"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="userId"
      @update:options="handlePagination"
    >
      <template #item.role="{ item }">
        {{ translateRole(item.role) }}
      </template>

      <template #item.actions="{ item }" dense>
        <v-btn
          @click="openEditDialog(item)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-pen"
        ></v-btn>

        <v-dialog max-width="500">
          <template #activator="{ props: activatorProps }">
            <v-btn
              rounded="lg"
              size="small"
              color="red"
              v-bind="activatorProps"
              class="ml-2"
              icon="mdi-delete"
            ></v-btn>
          </template>

          <template #default="{ isActive }">
            <v-card title="Potwierdź swoją decyzję">
              <v-card-text>
                Czy na pewno chcesz usunąć tego użytkownika?
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  text="Usuń użytkownika"
                  color="red"
                  rounded="lg"
                  @click="deleteUser(item.userId); isActive.value = false"
                ></v-btn>
                <v-btn
                  color="grey"
                  rounded="lg"
                  text="Anuluj"
                  @click="isActive.value = false"
                ></v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </v-dialog>

        <v-btn
          @click="openRoleDialog(item)"
          rounded="lg"
          size="small"
          color="teal"
          class="ml-2"
          icon="mdi-account-switch"
        ></v-btn>

        <v-btn
          @click="openStatusDialog(item)"
          rounded="lg"
          size="small"
          color="teal"
          class="ml-2"
          icon="mdi-shield-check"
        ></v-btn>
      </template>

      <template #item.isActive="{ item }">
        {{ item.isActive ? 'Tak' : 'Nie' }}
      </template>

      <template #item.createdAt="{ item }">
        {{ new Date(item.createdAt).toLocaleString() }}
      </template>
    </v-data-table-server>

    <v-dialog v-model="isEditDialogActive" max-width="500">
      <template #default>
        <v-card title="Edytuj użytkownika" rounded="lg">
          <GenerateUserForm
            :user="userToEdit"
            @on-valid-submit="updateUser"
            @on-invalid-submit="handleInvalidSubmit"
          ></GenerateUserForm>
        </v-card>
      </template>
    </v-dialog>
  </BasePage>
</template>
