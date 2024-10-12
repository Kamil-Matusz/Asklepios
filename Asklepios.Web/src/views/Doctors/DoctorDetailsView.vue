<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useMedicalStaffStore } from '@/stores/doctorStore';
import { useToast } from 'vue-toastification';
import { MedicalStaffListDto } from '@/models/Users/doctor';

const route = useRoute();
const router = useRouter();
const medicalStaffStore = useMedicalStaffStore();
const toast = useToast();

const doctor = ref<MedicalStaffListDto | null>(null);

const fetchDoctorDetails = async () => {
  try {
    const doctorId = route.params.id as string;
    await medicalStaffStore.dispatchGetDoctor(doctorId);
    doctor.value = medicalStaffStore.doctorDetails;
  } catch (error) {
    console.error('Error fetching doctor details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów lekarza');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(fetchDoctorDetails);
</script>

<template>
  <BasePage :title="`Szczegóły lekarza: ${doctor?.name} ${doctor?.surname}`">
    <div v-if="doctor" class="doctor-details">
      <h2>{{ doctor.name }} {{ doctor.surname }}</h2>
      <p><strong>Specjalizacja: </strong> {{ doctor.specialization }}</p>
      <p><strong>Nr. telefonu: </strong> {{ doctor.hospitalPhoneNumber }}</p>
      <p><strong>Prywatny Nr. Telefonu: </strong> {{ doctor.privatePhoneNumber }}</p>
      <p><strong>Licencja: </strong> {{ doctor.medicalLicenseNumber }}</p>

      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<style scoped>
.doctor-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  color: black;
}

.doctor-details h2 {
  margin-bottom: 10px;
}

.doctor-details p {
  margin-bottom: 5px;
}

.btn-custom {
  margin-top: 15px;
  padding: 10px 20px;
  border: 2px solid #4CAF50;
  border-radius: 5px;
  background-color: #4CAF50;
  color: white;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-custom:hover {
  background-color: #45a049;
  border-color: #45a049;
}
</style>
