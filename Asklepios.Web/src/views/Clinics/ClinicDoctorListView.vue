<template>
  <BasePage title="Lekarze przyjmujący w przychodni">
    <v-data-table-server
      class="custom-table-background"
      :headers="headers"
      :items="medicalStaffStore.clinicDoctors"
      :loading="loading"
      item-value="doctorId">
    </v-data-table-server>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useToast } from 'vue-toastification';
import BasePage from '@/components/pages/BasePage.vue';
import { useMedicalStaffStore } from '@/stores/doctorStore';

const medicalStaffStore = useMedicalStaffStore();
const toast = useToast();

const headers = [
  { title: 'Imię', key: 'name', align: 'start' },
  { title: 'Nazwisko', key: 'surname', align: 'start' },
  { title: 'Specjalizacja', key: 'specialization', align: 'start' },
];

const loading = ref(true);

const getDoctors = async () => {
  loading.value = true;
  try {
    await medicalStaffStore.dispatchGetClinicsDoctorsList();
  } catch (error) {
    console.error('Error fetching doctors:', error);
    toast.error('Wystąpił problem podczas pobierania lekarzy przychodni');
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  getDoctors();
});
</script>

<style scoped>
.custom-table-background {
  background-color: gainsboro;
  color: black;
}
</style>
