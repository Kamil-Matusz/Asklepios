<template>
  <v-container>
    <v-row justify="center" class="mt-5">
      <v-col cols="12" class="text-center">
        <h2>Archiwalne wizyty</h2>
      </v-col>
    </v-row>

    <v-row justify="center" v-if="appointments.length > 0">
      <v-col cols="12" sm="10" md="8">
        <v-card class="pa-4" color="grey darken-3" dark>
          <v-card-title>Szczegóły wizyty: <strong>{{ currentDate }}</strong></v-card-title>

          <v-card-text>
            <v-row v-for="appointment in appointments" :key="appointment.appointmentId">
              <v-col cols="12" class="mb-3">
                <v-card color="grey lighten-4" class="pa-3" elevation="0">
                  <v-row>
                    <v-col cols="12">
                      <strong>Pacjent:{{ appointment.patientName }} {{ appointment.patientSurname }}</strong>
                    </v-col>
                    <v-col cols="12">
                      <strong>Data wizyty:</strong> {{ formatDate(appointment.appointmentDate) }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Lekarz:</strong> {{ appointment.doctorName }} {{ appointment.doctorSurname }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Rodzaj wizyty:</strong> {{ translateAppointmentType(appointment.appointmentType.value) }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Status wizyty:</strong> {{ translateStatus(appointment.status) }}
                    </v-col>
                  </v-row>
                </v-card>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row justify="center" v-else>
      <v-col cols="12" sm="10" md="8">
        <v-alert type="info" border="left" colored-border>
          Brak archiwalnych wizyt.
        </v-alert>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useClinicAppointmentsStore } from '@/stores/clinicAppointmentStore';

const store = useClinicAppointmentsStore();
const appointments = ref([]);

const pastAppointments = async () => {
  try {
    const result = await store.dispatchGetUserPastAppointments();
    appointments.value = result;
  } catch (error) {
    console.error('Błąd podczas pobierania wizyt:', error);
  }
};

onMounted(() => {
  pastAppointments();
});

const translateStatus = (status: string) => {
  const statusTranslations: { [key: string]: string } = {
    'Scheduled': 'Zaplanowana',
    'Completed': 'Zakończona',
    'Canceled': 'Odwołana',
    'In Progress': 'W toku'
  };
  return statusTranslations[status] || status;
};

const translateAppointmentType = (appointmentType: string) => {
  const typeTranslations: { [key: string]: string } = {
    'Consultation': 'Konsultacja',
    'Examination': 'Badanie',
    'Surgery': 'Operacja/Zabieg',
  };
  return typeTranslations[appointmentType] || appointmentType;
};

const formatDate = (date) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
  return new Date(date).toLocaleDateString('pl-PL', options);
};
</script>
