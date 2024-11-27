<template>
  <BasePage title="Zapisz pacjenta na nową wizytę">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Zapisz nową wizytę pacjenta</v-card-title>
            <v-card-text>
              <v-form ref="form" v-model="isFormValid" lazy-validation>
                <v-text-field
                  v-model="appointmentRequest.patientName"
                  label="Imię pacjenta"
                  :rules="requiredRule"
                  prepend-icon="mdi-account"
                  required
                ></v-text-field>

                <v-text-field
                  v-model="appointmentRequest.patientSurname"
                  label="Nazwisko pacjenta"
                  :rules="requiredRule"
                  prepend-icon="mdi-account"
                  required
                ></v-text-field>

                <v-text-field
                  v-model="appointmentRequest.peselNumber"
                  label="Numer PESEL"
                  :rules="[requiredRule, validatePesel]"
                  prepend-icon="mdi-card-account-details"
                  required
                ></v-text-field>

                <v-text-field
                  v-model="appointmentRequest.contactNumber"
                  label="Numer kontaktowy"
                  :rules="requiredRule"
                  prepend-icon="mdi-phone"
                  required
                ></v-text-field>

                <v-text-field
                  v-model="appointmentRequest.email"
                  label="Email"
                  type="email"
                  :rules="[requiredRule, validateEmail]"
                  prepend-icon="mdi-email"
                  required
                ></v-text-field>

                <v-text-field
                  v-model="appointmentRequest.appointmentDate"
                  label="Data i godzina wizyty"
                  type="datetime-local"
                  prepend-icon="mdi-calendar"
                  :rules="[requiredRule, validateTimeInterval]"
                  required
                ></v-text-field>

                <v-select
                  v-model="appointmentRequest.appointmentType"
                  :items="appointmentTypes"
                  label="Typ spotkania"
                  item-title="text"
                  item-value="value"
                  :rules="requiredRule"
                  required
                  prepend-icon="mdi-stethoscope"
                ></v-select>

                <v-select
                  v-model="appointmentRequest.medicalStaffId"
                  :items="doctors"
                  label="Wybierz lekarza"
                  item-title="text"
                  item-value="value"
                  :rules="requiredRule"
                  prepend-icon="mdi mdi-doctor"
                  required
                ></v-select>

                <v-select
                  v-model="appointmentRequest.status"
                  :items="statusOptions"
                  label="Status"
                  item-title="text"
                  item-value="value"
                  :rules="requiredRule"
                  required
                  prepend-icon="mdi-information"
                ></v-select>

                <v-alert v-if="errorMessage" type="error" dense>{{ errorMessage }}</v-alert>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-btn color="green" @click="createAppointment" :disabled="!isFormValid">Dodaj spotkanie</v-btn>
              <v-btn color="red" @click="navigateBack">Anuluj</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue';
import { useClinicAppointmentsStore } from '@/stores/clinicAppointmentStore';
import { useRouter } from 'vue-router';
import BasePage from '@/components/pages/BasePage.vue';
import { useToast } from 'vue-toastification';

const appointmentRequest = reactive({
  patientName: '',
  patientSurname: '',
  peselNumber: '',
  contactNumber: '',
  email: '',
  appointmentDate: '',
  appointmentType: '',
  medicalStaffId: '',
  status: '',
});

const isFormValid = ref(false);
const errorMessage = ref('');
const toast = useToast();
const statusOptions = ref([
  { text: 'Potwierdzone', value: 'Scheduled' },
  { text: 'Zakończone', value: 'Completed' },
  { text: 'Odwołane', value: 'Cancelled' },
]);
const appointmentTypes = ref([
  { text: 'Konsultacja', value: 'Consultation' },
  { text: 'Badanie', value: 'Examination' },
  { text: 'Zabieg', value: 'Surgery' },
]);
const doctors = ref([
  { text: 'Kamil Matusz', value: 'e582299d-1a49-4d7b-8e36-eadb449dd209' },
  { text: 'Miłosz Michalski', value: 'e582299d-1a49-4d7b-8e36-eadb449dd212' },
]);
const requiredRule = [(v: string) => !!v || 'Pole wymagane'];

function validateTimeInterval(v: string) {
  const dateTime = new Date(v);
  const minutes = dateTime.getMinutes();

  if (minutes % 30 !== 0) {
    return 'Wybierz czas w odstępach co 30 minut (np. 08:00, 08:30)';
  }

  const hour = dateTime.getHours();
  if (hour < 8 || hour > 16 || (hour === 16 && minutes > 0)) {
    return 'Godzina spotkania musi być między 08:00 a 16:00';
  }

  return true;
}

const clinicAppointmentsStore = useClinicAppointmentsStore();
const router = useRouter();

async function createAppointment() {
  try {
    await clinicAppointmentsStore.dispatchCreateAppointment(appointmentRequest);
    toast.success('Pomyślnie dodano spotkanie!');
    router.push('/clinicDashboard');
  } catch (error) {
    console.error('Error creating appointment:', error);
    errorMessage.value = 'Nie udało się dodać spotkania. Spróbuj ponownie.';
  }
}

function navigateBack() {
  router.back();
}

function validatePesel(v: string) {
  return /^\d{11}$/.test(v) || 'Nieprawidłowy numer PESEL';
}

function validateEmail(v: string) {
  const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return pattern.test(v) || 'Nieprawidłowy email';
}
</script>

<style scoped>
.v-card {
  background-color: white;
  color: black;
}
</style>
