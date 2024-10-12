<template>
  <BasePage title="Zarządzanie lekarzami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 25rem">
            + Zarejestruj w systemie nowego lekarza
          </v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Uzupełnij dane" rounded="lg">
            <CreateDoctorForm
              v-model="doctorToAdd"
              :departments="departments"
              :users="users"
              @on-valid-submit="(doctor) => { addDoctor(doctor); isActive.value = false; }"
            ></CreateDoctorForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-container>
      <v-row>
        <v-col v-for="doctor in doctors" :key="doctor.doctorId" cols="12" md="6" lg="4">
          <v-card>
            <v-card-title>{{ doctor.name }} {{ doctor.surname }}</v-card-title>
            <v-card-text>
              <div>Oddział: {{ doctor.departmentName }}</div>
              <div>Specjalizacja: {{ doctor.specialization }}</div>
              <div>Prywatny numer: {{ doctor.privatePhoneNumber }}</div>
              <div>Numer w szpitalu: {{ doctor.hospitalPhoneNumber }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="blue" text @click="goToDetails(doctor.doctorId)">Szczegóły</v-btn>
              <v-btn color="primary" text @click="goToEdit(doctor.doctorId)">Edytuj</v-btn>
              <v-btn color="red" text @click="deleteDoctor(doctor.doctorId)">Usuń</v-btn>
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
            @change="getDoctors"
          ></v-select>
        </v-col>
      </v-row>

      <v-pagination
        v-model="currentPage"
        :length="Math.ceil(totalDoctors / itemsPerPage)"
        @input="getDoctors"
        color="blue"
      ></v-pagination>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { useMedicalStaffStore } from '@/stores/doctorStore';
import { useDepartmentStore } from '@/stores/departmentStore';
import { useUserStore } from '@/stores/userStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import CreateDoctorForm from '@/components/doctors/CreateDoctorForm.vue';
import { InputCreateMedicalStaff } from '@/models/Users/doctor';

const medicalStaffStore = useMedicalStaffStore();
const departmentStore = useDepartmentStore();
const userStore = useUserStore();
const toast = useToast();
const router = useRouter();

const currentPage = ref(1);
const itemsPerPage = ref(10);
const itemsPerPageOptions = ref([5, 10, 20, 50]);

const doctors = ref([]);
const totalDoctors = ref(0);

const doctorToAdd = ref(new InputCreateMedicalStaff());

const departments = ref([]);
const users = ref([]);

const getDoctors = async () => {
  try {
    const paginationParams = {
      pageIndex: currentPage.value,
      pageSize: itemsPerPage.value
    };
    await medicalStaffStore.dispatchGetDoctors(paginationParams);
    doctors.value = medicalStaffStore.doctors;
    totalDoctors.value = medicalStaffStore.totalItems;
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania danych lekarzy');
  }
};

const addDoctor = async (doctor) => {
  try {
    await medicalStaffStore.dispatchCreateDoctor(doctor);
    toast.success('Pomyślnie dodano nowego lekarza');
    getDoctors();
  } catch (error) {
    toast.error('Wystąpił problem podczas dodawania lekarza');
  }
};

const deleteDoctor = async (id) => {
  try {
    await medicalStaffStore.dispatchDeleteDoctor(id);
    toast.success('Pomyślnie usunięto lekarza');
    getDoctors();
  } catch (error) {
    toast.error('Wystąpił problem podczas usuwania lekarza');
  }
};

const goToDetails = (id) => {
  router.push(`/doctor/${id}`);
};

const goToEdit = (id) => {
  router.push({ name: 'DoctorEdit', params: { id } });
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
    const data = await userStore.dispatchGetDoctors();
    users.value = data.map(user => ({
      email: user.email,
      userId: user.userId
    }));
  } catch (error) {
    console.error('Error fetching users:', error);
    toast.error('Wystąpił problem podczas pobierania użytkowników');
  }
};

onMounted(() => {
  getDoctors();
  getDepartments();
  getUsers();
});

watch([currentPage, itemsPerPage], getDoctors);
</script>

