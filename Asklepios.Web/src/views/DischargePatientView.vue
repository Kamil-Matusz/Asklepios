<template>
  <BasePage title="Wypisanie pacjenta">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>
              Wypisanie pacjenta
            </v-card-title>
            <v-card-text>
              <v-form @submit.prevent="handleSubmit">
                <v-text-field
                  v-model="form.patientName"
                  label="Imię"
                  disabled
                ></v-text-field>
                <v-text-field
                  v-model="form.patientSurname"
                  label="Nazwisko"
                  disabled
                ></v-text-field>
                <v-text-field
                  v-model="form.peselNumber"
                  label="Pesel"
                  disabled
                ></v-text-field>
                <v-text-field
                  v-model="form.address"
                  label="Adres"
                ></v-text-field>
                <v-date-picker
                  v-model="form.date"
                  label="Data"
                  :rules="dateRules"
                  required
                ></v-date-picker>
                <v-text-field
                  v-model="form.dischargeReasson"
                  label="Powód wypisu"
                  :rules="dischargeReassonRules"
                  required
                ></v-text-field>
                <v-text-field
                  v-model="form.summary"
                  label="Podsumowanie wypisu"
                  :rules="summaryRules"
                  required
                ></v-text-field>
                <v-text-field
                  v-model="form.medicalStaffId"
                  label="ID personelu medycznego"
                  :rules="medicalStaffIdRules"
                  required
                ></v-text-field>
                <v-btn type="submit" color="green">Wypisz pacjenta</v-btn>
              </v-form>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useDischargeStore } from '@/stores/dischargeStore';
import { usePatientStore } from '@/stores/patientStore';
import { useToast } from 'vue-toastification';
import { DischargePersonDto} from '@/models/Patients/discharge';

const route = useRoute();
const router = useRouter();
const toast = useToast();
const dischargeStore = useDischargeStore();
const patientStore = usePatientStore();

const form = ref<DischargePersonDto>({
  patientName: '',
  patientSurname: '',
  peselNumber: '',
  address: '',
  date: new Date(),
  dischargeReasson: '',
  summary: '',
  medicalStaffId: '',
});

const fetchPatientDetails = async () => {
  try {
    const patientDetails = await patientStore.dispatchGetPatient(route.params.id as string);
    if (patientDetails) {
      form.value.patientName = patientDetails.patientName;
      form.value.patientSurname = patientDetails.patientSurname;
      form.value.peselNumber = patientDetails.peselNumber;
      form.value.address = patientDetails.address;
    }
  } catch (error) {
    console.error('Error fetching patient details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pacjenta');
  }
};

const dateRules = [(v: Date) => !!v || 'Data jest wymagana'];
const dischargeReassonRules = [(v: string) => !!v || 'Powód wypisu jest wymagany'];
const summaryRules = [(v: string) => !!v || 'Podsumowanie jest wymagane'];
const medicalStaffIdRules = [(v: string) => !!v || 'ID personelu medycznego jest wymagane'];

const handleSubmit = async () => {
  try {
    await dischargeStore.dispatchDischargePatient(form.value);
    toast.success('Pacjent został pomyślnie wypisany');
    router.push({ name: 'PatientManagement' });
  } catch (error) {
    console.error('Error discharging patient:', error);
    toast.error('Wystąpił problem podczas wypisywania pacjenta');
  }
};

onMounted(() => {
  fetchPatientDetails();
});
</script>
