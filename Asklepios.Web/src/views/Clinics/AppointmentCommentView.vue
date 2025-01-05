<template>
  <BasePage title="Dodaj komentarz do wizyty">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Dodaj komentarz do wizyty</v-card-title>
            <v-card-text>
              <v-form ref="form" v-model="isFormValid" lazy-validation>
                <v-textarea
                  v-model="detailsRequest.description"
                  label="Opis wizyty"
                  :rules="requiredRule"
                  rows="5"
                  required
                  prepend-icon="mdi mdi-note"
                ></v-textarea>

                <v-textarea
                  v-model="detailsRequest.recommendations"
                  label="Rekomendacje"
                  :rules="requiredRule"
                  rows="4"
                  required
                  prepend-icon="mdi mdi-note"
                ></v-textarea>

                <v-textarea
                  v-model="detailsRequest.prescriptions"
                  label="Zapisane leki"
                  :rules="requiredRule"
                  rows="3"
                  required
                  prepend-icon="mdi mdi-note"
                ></v-textarea>

                <v-textarea
                  v-model="detailsRequest.doctorComments"
                  label="Ewentualny komentarz"
                  :rules="requiredRule"
                  rows="1"
                  required
                  prepend-icon="mdi mdi-note"
                ></v-textarea>

                <v-alert v-if="errorMessage" type="error" dense>{{ errorMessage }}</v-alert>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-btn color="green" @click="createAppointmentDetails" :disabled="!isFormValid">Dodaj szczegóły</v-btn>
              <v-btn color="red" @click="navigateBack">Anuluj</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import { useClinicAppointmentDetailsStore } from '@/stores/clinicAppointmentDetailsStore';
import { useRouter, useRoute } from 'vue-router';
import BasePage from '@/components/pages/BasePage.vue';
import { useToast } from 'vue-toastification';

const detailsRequest = reactive({
  description: '',
  recommendations: '',
  prescriptions: '',
  doctorComments: '',
  appointmentId: '',
});

const isFormValid = ref(false);
const errorMessage = ref('');
const toast = useToast();
const requiredRule = [(v: string) => !!v || 'Pole wymagane'];

const clinicAppointmentDetailsStore = useClinicAppointmentDetailsStore();
const router = useRouter();
const route = useRoute();

onMounted(() => {
  detailsRequest.appointmentId = route.params.id as string || '';
  if (!detailsRequest.appointmentId) {
    console.error('Brak ID wizyty w URL.');
    errorMessage.value = 'Brak ID wizyty. Spróbuj ponownie.';
  }
});

async function createAppointmentDetails() {
  if (!detailsRequest.appointmentId) {
    errorMessage.value = 'Brak ID wizyty. Spróbuj ponownie.';
    return;
  }

  try {
    await clinicAppointmentDetailsStore.dispatchCreateAdmissionDetails(detailsRequest);
    toast.success('Pomyślnie dodano szczegóły wizyty!');
    router.push('/userFutureAppointments');
  } catch (error) {
    console.error('Błąd podczas dodawania szczegółów wizyty:', error);
    errorMessage.value = 'Nie udało się dodać szczegółów wizyty. Spróbuj ponownie.';
  }
}

function navigateBack() {
  router.back();
}
</script>

<style scoped>
.v-card {
  background-color: white;
  color: black;
}
</style>
