<template>
  <BasePage title="Edycja pacjenta">
    <div v-if="form.patientId !== ''" class="patient-details">
      <h2>Edycja pacjenta {{ form.patientName }} {{ form.patientSurname }}</h2>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field
          label="Imię"
          v-model="form.patientName"
          required
        ></v-text-field>
        <v-text-field
          label="Nazwisko"
          v-model="form.patientSurname"
          required
        ></v-text-field>
        <v-text-field
          label="Numer PESEL"
          v-model="form.peselNumber"
          required
        ></v-text-field>
        <v-text-field
          label="Początkowa diagnoza"
          v-model="form.initialDiagnosis"
          required
        ></v-text-field>
        <v-text-field
          label="Leczenie"
          v-model="form.treatment"
          required
        ></v-text-field>
        <v-select
          v-model="form.departmentId"
          label="Oddział"
          :items="formattedDepartments"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-select
          v-model="form.roomId"
          label="Pokój"
          :items="formattedRooms"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-btn type="submit" color="primary">Zatwierdź</v-btn>
      </v-form>
      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { usePatientStore } from '@/stores/patientStore';
import { useToast } from 'vue-toastification';
import { PatientDto } from '@/models/Patients/patient';

const route = useRoute();
const router = useRouter();
const patientStore = usePatientStore();
const toast = useToast();

const form = ref<PatientDto>({
  patientId: '',
  patientName: '',
  patientSurname: '',
  peselNumber: '',
  initialDiagnosis: '',
  isDischarged: false,
  treatment: '',
  departmentId: '',
  roomId: '',
});

const fetchPatientDetails = async () => {
  try {
    const patientId = route.params.id as string;
    await patientStore.dispatchGetPatient(patientId);
    const patient = patientStore.patientDetails;
    if (patient) {
      form.value = { ...patient };
    }
  } catch (error) {
    console.error('Error fetching patient details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pacjenta');
  }
};

const formattedDepartments = computed(() => {
  return patientStore.departments?.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  })) || [];
});

const formattedRooms = computed(() => {
  return patientStore.rooms?.map(room => ({
    text: room.roomNumber,
    value: room.roomId
  })) || [];
});

const handleSubmit = async () => {
  try {
    console.log('Dane formularza:', form.value);
    if (!form.value.patientName || !form.value.patientSurname || !form.value.peselNumber || !form.value.initialDiagnosis || !form.value.treatment || !form.value.departmentId || !form.value.roomId) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    await patientStore.dispatchUpdatePatient(form.value.patientId || '', form.value);
    toast.success('Pomyślnie zaktualizowano dane pacjenta!');
    router.go(-1);
  } catch (error) {
    console.error('Error updating patient:', error);
    toast.error('Wystąpił problem podczas aktualizacji pacjenta');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(async () => {
  await patientStore.dispatchGetDepartmentsAutocomplete();
  await patientStore.dispatchGetRoomsList();
  await fetchPatientDetails();
});
</script>

<style scoped>
.patient-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  color: black;
}

.patient-details h2 {
  margin-bottom: 10px;
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
