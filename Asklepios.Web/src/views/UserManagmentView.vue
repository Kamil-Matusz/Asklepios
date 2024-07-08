<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { useToast } from 'vue-toastification';
import { User, GenerateUserAccount } from '@/models/Users/user';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import GenerateUserForm from '@/components/users/GenerateUserForm.vue';

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

const addUser = async () => {
  await usersStore.dispatchCreateUser(userToAdd.value);
  toast.success('Pomyślnie dodano nowego użytkownika!');
  userToAdd.value = { email: '', password: '', role: '' };
  getUsers();
};

const deleteUser = async (id: string) => {
  await usersStore.dispatchDeleteUser(id);
  toast.success('Pomyślnie usunięto użytkownika!');
  getUsers();
};

const updateUser = async () => {
  await usersStore.updateUser(userToEdit.value);
  toast.success('Pomyślnie zaktualizowano dane użytkownika!');
  getUsers();
};

const openEditDialog = (user: User) => {
  userToEdit.value = { ...user };
  numToEdit.value = user.userId;
  isActive.value = true;
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
            color="primary"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem"
          >+Dodaj nowego użytkownika</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowy użytkownik" rounded="lg">
            <GenerateUserForm
              v-model="userToAdd"
              @on-valid-submit="() => { addUser(); isActive.value = false; }"
            ></GenerateUserForm>
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
                  @click="() => { isActive.value = false; deleteUser(item.userId); }"
                ></v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </v-dialog>
      </template>

      <template #item.isActive="{ item }">
        {{ item.isActive ? 'Tak' : 'Nie' }}
      </template>

      <template #item.createdAt="{ item }">
        {{ new Date(item.createdAt).toLocaleString() }}
      </template>
    </v-data-table-server>

    <v-dialog max-width="500" v-model="isActive">
      <template #default>
        <v-card title="Edytuj użytkownika" rounded="lg">
          <GenerateUserForm
            v-model="userToEdit"
            @on-valid-submit="() => { updateUser(); isActive.value = false; }"
          ></GenerateUserForm>
        </v-card>
      </template>
    </v-dialog>
  </BasePage>
</template>
