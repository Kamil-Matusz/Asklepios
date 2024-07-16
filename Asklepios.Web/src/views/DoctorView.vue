<template>
  <BasePage title="Zarządzanie lekarzami">
    <v-container>
      <v-row>
        <v-col v-for="doctor in doctors" :key="doctor.doctorId" cols="12" md="6" lg="4">
          <v-card class="doctor-card">
            <v-card-title>{{ doctor.name }} {{ doctor.surname }}</v-card-title>
            <v-card-text class="text-justify">
              <div><strong>Specjalizacja:</strong> {{ doctor.specialization }}</div>
              <div><strong>Nr. telefonu:</strong> {{ doctor.hospitalPhoneNumber }}</div>
              <div><strong>Oddział:</strong> {{ doctor.departmentName }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="blue" text @click="goToDetails(doctor.doctorId)">Szczegóły</v-btn>
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
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';

const medicalStaffStore = useMedicalStaffStore();
const toast = useToast();
const router = useRouter();

const currentPage = ref(1);
const itemsPerPage = ref(10);
const itemsPerPageOptions = ref([5, 10, 20, 50]);

const doctors = ref([]);
const totalDoctors = ref(0);

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

const deleteDoctor = async (id: string) => {
  try {
    await medicalStaffStore.dispatchDeleteDoctor(id);
    toast.success('Pomyślnie usunięto lekarza');
    getDoctors();
  } catch (error) {
    toast.error('Wystąpił problem podczas usuwania lekarza');
  }
};

const goToDetails = (id: string) => {
  router.push(`/doctor/${id}`);
};

onMounted(getDoctors);

watch([currentPage, itemsPerPage], getDoctors);
</script>

<style scoped>
.doctor-card {
  margin-bottom: 20px;
}

.text-justify {
  text-align: justify;
}
</style>
