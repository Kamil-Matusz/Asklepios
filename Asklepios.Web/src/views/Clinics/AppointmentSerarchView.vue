<template>
  <v-container>
    <v-row justify="center" class="mt-5">
      <v-col cols="12" class="text-center">
        <h2>Wyszukiwanie wizyt po dacie</h2>
      </v-col>
    </v-row>

    <v-row justify="center" class="mt-3">
      <v-col cols="12" sm="8" md="6">
        <v-text-field
          v-model="selectedDate"
          label="Podaj datę (YYYY-MM-DD)"
          outlined
          clearable
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="4" md="2">
        <v-btn @click="searchAppointmentsByDate" color="yellow" dark class="ma-4">Szukaj</v-btn>
      </v-col>
    </v-row>

    <v-row justify="center" v-if="appointments.length > 0">
      <v-col cols="12" sm="10" md="8">
        <v-card class="pa-4" color="grey darken-3" dark>
          <v-card-title>Wizyty z dnia: <strong>{{ selectedDate }}</strong></v-card-title>

          <v-card-text>
            <v-row v-for="appointment in appointments" :key="appointment.appointmentId">
              <v-col cols="12" class="mb-3">
                <v-card color="grey lighten-4" class="pa-3" outlined>
                  <v-row>
                    <v-col cols="12">
                      <strong>Imię pacjenta:</strong> {{ appointment.patientName }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Nazwisko pacjenta:</strong> {{ appointment.patientSurname }}
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
                    <v-col cols="12" class="text-right">
                      <v-btn @click="deleteAppointment(appointment.appointmentId)" color="red" dark>Usuń</v-btn>
                    </v-col>
                  </v-row>
                </v-card>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row justify="center" v-if="appointments.length === 0 && searched">
      <v-col cols="12" sm="10" md="8">
        <v-alert type="info" border="left" colored-border>
          Brak wizyt na podaną datę.
        </v-alert>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useClinicAppointmentsStore } from '@/stores/clinicAppointmentStore';

const store = useClinicAppointmentsStore();

const selectedDate = ref('');
const appointments = ref([]);
const searched = ref(false);

const searchAppointmentsByDate = async () => {
  if (selectedDate.value) {
    try {
      const result = await store.dispatchGetAppointmentsByDate(selectedDate.value);
      console.log("Wynik wyszukiwania:", result);
      appointments.value = result || [];
      searched.value = true;
    } catch (error) {
      console.error("Błąd podczas wyszukiwania wizyt:", error);
    }
  }
};

const deleteAppointment = async (appointmentId) => {
  try {
    await store.dispatchDeleteAppointment(appointmentId);
    await searchAppointmentsByDate();
  } catch (error) {
    console.error("Błąd podczas usuwania wizyty:", error);
  }
};

const formatDate = (date) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
  return new Date(date).toLocaleDateString('pl-PL', options);
};

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
</script>

<style scoped>
.v-card {
  margin-top: 20px;
  border-radius: 15px;
}

.v-card-title {
  font-size: 1.25rem;
  font-weight: bold;
}

.v-row {
  margin-bottom: 10px;
}

.v-col {
  padding: 5px 0;
}

.v-btn {
  font-weight: bold;
  border-radius: 8px;
}

.v-alert {
  font-weight: bold;
  margin-top: 20px;
}

h2 {
  font-size: 1.5rem;
  font-weight: bold;
  color: whitesmoke;
  margin-bottom: 20px;
}
</style>
