<template>
  <BasePage title="Zarządzanie lekarzami">
    <v-container>
      <v-row>
        <v-col v-for="doctor in doctors" :key="doctor.doctorId" cols="12" md="6" lg="4">
          <v-card>
            <v-card-title>{{ doctor.name }} {{ doctor.surname }}</v-card-title>
            <v-card-text>
              <div>Specjalizacja: {{ doctor.specialization }}</div>
              <div>Nr. telefonu: {{ doctor.hospitalPhoneNumber }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="blue" text @click="goToDetails(doctor.doctorId)">Szczegóły</v-btn>
              <v-btn color="red" text @click="deleteDoctor(doctor.doctorId)">Usuń</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useMedicalStaffStore } from '@/stores/doctorStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';

const medicalStaffStore = useMedicalStaffStore();
const toast = useToast();
const router = useRouter();

const doctors = ref([]);

const getDoctors = async () => {
  try {
    await medicalStaffStore.dispatchGetDoctors({ pageIndex: 1, pageSize: 10 });
    doctors.value = medicalStaffStore.doctors;
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
</script>
