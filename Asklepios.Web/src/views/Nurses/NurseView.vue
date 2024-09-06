<template>
  <BasePage title="Zarządzanie pielęgniarkami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem"
          >
            +Dodaj nową pielęgniarkę
          </v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowa pielęgniarka" rounded="lg">
            <NurseForm
              v-model="nurseToAdd"
              :departments="departments"
              :users="users"
              @on-valid-submit="(nurse) => { addNurse(nurse); isActive.value = false; }"
            ></NurseForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-container>
      <v-row>
        <v-col v-for="nurse in nurses" :key="nurse.nurseId" cols="12" md="6" lg="4">
          <v-card>
            <v-card-title>{{ nurse.name }} {{ nurse.surname }}</v-card-title>
            <v-card-text>
              <div>Oddział: {{ nurse.departmentName }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" text @click="goToEdit(nurse.nurseId)">Edytuj</v-btn>
              <v-btn color="red" text @click="deleteNurse(nurse.nurseId)">Usuń</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12">
          <v-select
            v-model="itemsPerPage"
            :items="itemsPerPageOptions"
            label="Liczba obiektów na stronę"
            @change="getNurses"
          ></v-select>
        </v-col>
      </v-row>

      <v-pagination
        v-model="currentPage"
        :length="Math.ceil(totalNurses / itemsPerPage)"
        @input="getNurses"
        color="blue"
      ></v-pagination>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { useNurseStore } from '@/stores/nurseStore';
import { useDepartmentStore } from '@/stores/departmentStore';
import { useUserStore } from '@/stores/userStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import NurseForm from '@/components/nurses/CreateNurseForm.vue';
import { InputCreateNurse } from '@/models/Users/nurse';

const nurseStore = useNurseStore();
const departmentStore = useDepartmentStore();
const userStore = useUserStore();
const toast = useToast();
const router = useRouter();

const currentPage = ref(1);
const itemsPerPage = ref(10);
const itemsPerPageOptions = ref([5, 10, 20, 50]);

const nurses = ref([]);
const totalNurses = ref(0);

const nurseToAdd = ref(new InputCreateNurse());

const departments = ref([]);
const users = ref([]);

const getNurses = async () => {
  try {
    const paginationParams = {
      pageIndex: currentPage.value,
      pageSize: itemsPerPage.value
    };
    await nurseStore.dispatchGetNurses(paginationParams);
    nurses.value = nurseStore.nurses;
    totalNurses.value = nurseStore.totalItems;
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania danych pielęgniarek');
  }
};

const addNurse = async (nurse) => {
  try {
    await nurseStore.dispatchCreateNurse(nurse);
    toast.success('Pomyślnie dodano nową pielęgniarkę');
    getNurses();
  } catch (error) {
    toast.error('Wystąpił problem podczas dodawania pielęgniarki');
  }
};

const deleteNurse = async (id) => {
  try {
    await nurseStore.dispatchDeleteNurse(id);
    toast.success('Pomyślnie usunięto pielęgniarkę');
    getNurses();
  } catch (error) {
    toast.error('Wystąpił problem podczas usuwania pielęgniarki');
  }
};

const goToDetails = (id) => {
  router.push(`/nurse/${id}`);
};

const goToEdit = (id) => {
  router.push(`/nurse/edit/${id}`);
};

const getDepartments = async () => {
  try {
    const data = await departmentStore.dispatchGetDepartmentsAutocomplete();
    departments.value = data.map(department => ({
      departmentName: department.departmentName,
      departmentId: department.departmentId
    }));
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania oddziałów');
  }
};

const getUsers = async () => {
  try {
    const data = await userStore.dispatchGetNurses();
    users.value = data.map(user => ({
      email: user.email,
      userId: user.userId
    }));
  } catch (error) {
    console.error('Error fetching users:', error);
    toast.error('Wystąpił problem podczas pobierania uzytkownikow');
  }
};

onMounted(() => {
  getNurses();
  getDepartments();
  getUsers();
});

watch([currentPage, itemsPerPage], getNurses);
</script>

<style scoped>
.nurse-card {
  margin-bottom: 20px;
}
</style>
