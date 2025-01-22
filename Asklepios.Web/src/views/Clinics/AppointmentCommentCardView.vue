<template>
  <BasePage title="Szczegóły wizyty">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Szczegóły wizyty</v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <p><strong>Opis wizyty:</strong> {{ appointmentDetails?.description || 'Brak danych' }}</p>
                  <p><strong>Rekomendacje:</strong> {{ appointmentDetails?.recommendations || 'Brak danych' }}</p>
                  <p><strong>Zapisane leki:</strong> {{ appointmentDetails?.prescriptions || 'Brak danych' }}</p>
                  <p><strong>Komentarz lekarza:</strong> {{ appointmentDetails?.doctorComments || 'Brak danych' }}</p>
                </v-col>
              </v-row>
              <v-alert v-if="errorMessage" type="error" dense>{{ errorMessage }}</v-alert>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" @click="navigateBack">Powrót</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useClinicAppointmentDetailsStore } from '@/stores/clinicAppointmentDetailsStore';
import BasePage from '@/components/pages/BasePage.vue';

const route = useRoute();
const router = useRouter();
const clinicAppointmentDetailsStore = useClinicAppointmentDetailsStore();

const appointmentDetails = computed(() => clinicAppointmentDetailsStore.admissionDetails);
const errorMessage = ref('');

onMounted(async () => {
  const appointmentId = route.params.id as string;

  if (!appointmentId) {
    errorMessage.value = 'Nie znaleziono ID wizyty.';
    return;
  }

  try {
    await clinicAppointmentDetailsStore.dispatchGetAdmissionDetailsById(appointmentId);

    if (!clinicAppointmentDetailsStore.admissionDetails) {
      errorMessage.value = 'Nie udało się załadować szczegółów wizyty.';
    }
  } catch (error) {
    console.error('Błąd podczas ładowania szczegółów wizyty:', error);
    errorMessage.value = 'Wystąpił błąd podczas ładowania danych.';
  }
});

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
