<template>
  <BasePage title="Edycja pacjenta">
    <div v-if="form.patientName !== ''" class="patient-details">
      <h2>Edycja pacjenta {{ form.patientName }} {{ form.patientSurname }}</h2>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field
          label="Imię pacjenta"
          v-model="form.patientName"
          required
        ></v-text-field>
        <v-text-field
          label="Nazwisko pacjenta"
          v-model="form.patientSurname"
          required
        ></v-text-field>
        <v-text-field
          label="Numer telefonu"
          v-model="form.contactNumber"
          required
        ></v-text-field>
        <v-text-field
          label="Adres email"
          v-model="form.email"
          required
        ></v-text-field>
        <v-btn type="submit" color="primary">Zatwierdź</v-btn>
      </v-form>
      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useClinicPatientsStore } from '@/stores/clinicPatientStore';
import { useToast } from 'vue-toastification';

const route = useRoute();
const router = useRouter();
const clinicPatientStore = useClinicPatientsStore();
const toast = useToast();

const form = ref({
  patientId: '',
  patientName: '',
  patientSurname: '',
  contactNumber: '',
  email: ''
});

const fetchPatientDetails = async () => {
  try {
    const patientId = route.params.id as string;
    console.log('ID pacjenta z trasy:', patientId);

    if (!patientId) {
      throw new Error('Brak ID pacjenta w trasie');
    }

    const patient = await clinicPatientStore.dispatchGetClinicPatient(patientId);
    form.value = { ...patient };

    console.log('Pobrane dane pacjenta:', form.value);
  } catch (error) {
    console.error('Error fetching patient details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pacjenta');
  }
};

const handleSubmit = async () => {
  try {
    console.log('Dane w formularzu przed aktualizacją:', form.value);
    if (!form.value.clinicPatientId) {
      throw new Error('Brak ID pacjenta');
    }

    if (!form.value.patientName || !form.value.patientSurname) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }

    await clinicPatientStore.dispatchUpdateClinicPatient(form.value.clinicPatientId, form.value);
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
  await fetchPatientDetails();
});
</script>

<style scoped>
.patient-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
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
