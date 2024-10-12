<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useDepartmentStore } from '@/stores/departmentStore';
import { useToast } from 'vue-toastification';
import { type DepartmentDto, type DepartmentDetailsDto } from '@/models/Departments/department';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import DepartmentForm from '@/components/departments/DepartmentForm.vue';
import type { User } from '@/models/Users/user';
import { useJwtStore } from '@/stores/jwtStore';

const router = useRouter();
const departmentStore = useDepartmentStore();
const toast = useToast();
const { getUserRole, getUser } = useJwtStore();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Nazwa', key: 'departmentName', align: 'start' },
  { title: 'Liczba łóżek', key: 'numberOfBeds', align: 'start' },
  { title: 'Aktualna liczba pacjentów', key: 'actualNumberOfPatients', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const departmentToAdd = ref<DepartmentDto>({
  departmentId: '',
  departmentName: '',
  numberOfBeds: 0,
  actualNumberOfPatients: 0
});

const departmentToEdit = ref<DepartmentDetailsDto | null>(null);
const isEditDialogActive = ref(false);
const user = ref<User | null>(null);
const role = ref<string | null>(null);

const getDepartments = async () => {
  options.value.loading = true;
  try {
    await departmentStore.dispatchGetDepartments({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = departmentStore.totalItems;
  } catch (error) {
    console.error('Error fetching departments:', error);
    toast.error('Wystąpił problem podczas pobierania oddziałów');
  } finally {
    options.value.loading = false;
  }
};

const addDepartment = async (department: DepartmentDto) => {
  try {
    if (!department.departmentName || department.numberOfBeds === 0) {
      console.error('Wymagane są wszystkie pola: Nazwa i Liczba łóżek');
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    console.log('Dodawanie oddziału z danymi:', department);
    await departmentStore.dispatchCreateDepartment(department);
    toast.success('Pomyślnie dodano nowy oddział!');
    departmentToAdd.value = { departmentId: '', departmentName: '', numberOfBeds: 0, actualNumberOfPatients: 0 };
    getDepartments();
  } catch (error) {
    console.error('Error adding department:', error);
    toast.error('Wystąpił problem podczas dodawania oddziału');
  }
};

const deleteDepartment = async (id: string) => {
  await departmentStore.dispatchDeleteDepartment(id);
  toast.success('Pomyślnie usunięto oddział!');
  getDepartments();
};

const updateDepartment = async () => {
  if (departmentToEdit.value) {
    await departmentStore.dispatchUpdateDepartment(departmentToEdit.value.departmentId, departmentToEdit.value);
    toast.success('Pomyślnie zaktualizowano dane oddziału!');
    getDepartments();
  }
};

const redirectToEditPage = (id: string) => {
  router.push({ name: 'DepartmentEdit', params: { id } });
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getDepartments();
};

onMounted(() => {
  getDepartments();
  user.value = getUser();
  role.value = getUserRole();
});
</script>

<template>
  <BasePage title="Zarządzanie oddziałami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-if="user && (role === 'Admin' || role === 'IT Employee')"
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem">
            + Utwórz nowy oddział</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Uzupełnij formularz z danymi oddziału" rounded="lg">
            <DepartmentForm
              v-model="departmentToAdd"
              @on-valid-submit="(department) => { addDepartment(department); isActive.value = false; }"
            ></DepartmentForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="departmentStore.departments"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="departmentId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="() => redirectToEditPage(item.departmentId)"
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
                Czy na pewno chcesz usunąć ten oddział?
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  text="Usuń oddział"
                  color="red"
                  rounded="lg"
                  @click="() => { isActive.value = false; deleteDepartment(item.departmentId); }"
                ></v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </v-dialog>
      </template>

      <template #item.numberOfBeds="{ item }">
        {{ item.numberOfBeds }}
      </template>

      <template #item.actualNumberOfPatients="{ item }">
        {{ item.actualNumberOfPatients }}
      </template>
    </v-data-table-server>
  </BasePage>
</template>
