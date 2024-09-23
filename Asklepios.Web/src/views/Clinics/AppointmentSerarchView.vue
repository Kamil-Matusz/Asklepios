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
                    <v-col cols="12">
                      <v-btn v-if="!editingStatus[appointment.appointmentId]" @click="toggleEditStatus(appointment.appointmentId)" color="blue" dark>
                        Zmień status
                      </v-btn>

                      <div v-else>
                        <v-select
                          v-model="appointment.status"
                          :items="statusOptions"
                          label="Status"
                          item-title="text"
                          item-value="value"
                        ></v-select>
                        <v-btn @click="saveStatus(appointment)" color="green" dark>Zapisz</v-btn>
                        <v-btn @click="cancelStatusChange(appointment)" color="grey" dark>Anuluj</v-btn>
                      </div>
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

const editingStatus = ref<{ [key: string]: boolean }>({});
const originalStatuses = ref<{ [key: string]: string }>({});

const statusOptions = ref([
  { text: 'Potwierdzone', value: 'Scheduled' },
  { text: 'Zakończone', value: 'Completed' },
  { text: 'Odwołane', value: 'Cancelled' },
]);

const searchAppointmentsByDate = async () => {
  if (selectedDate.value) {
    try {
      const result = await store.dispatchGetAppointmentsByDate(selectedDate.value);
      appointments.value = result || [];
      searched.value = true;
    } catch (error) {
      console.error("Błąd podczas wyszukiwania wizyt:", error);
    }
  }
};

const toggleEditStatus = (appointmentId) => {
  if (!editingStatus.value[appointmentId]) {
    const appointment = appointments.value.find(a => a.appointmentId === appointmentId);
    if (appointment) {
      originalStatuses.value[appointmentId] = appointment.status;
    }
  }
  editingStatus.value[appointmentId] = !editingStatus.value[appointmentId];
};

const saveStatus = async (appointment) => {
  try {
    const statusDto = { status: appointment.status };
    await store.dispatchUpdateAppointmentStatus(appointment.appointmentId, statusDto);
    toggleEditStatus(appointment.appointmentId);
    await searchAppointmentsByDate();
    console.log('Status wizyty został zaktualizowany.');
  } catch (error) {
    console.error('Błąd podczas aktualizacji statusu wizyty:', error);
  }
};

const cancelStatusChange = (appointment) => {
  const originalStatus = originalStatuses.value[appointment.appointmentId];
  if (originalStatus) {
    appointment.status = originalStatus;
  }
  toggleEditStatus(appointment.appointmentId);
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

const translateAppointmentType = (appointmentType: string) => {
  const typeTranslations: { [key: string]: string } = {
    'Consultation': 'Konsultacja',
    'Examination': 'Badanie',
    'Surgery': 'Operacja/Zabieg',
  };
  return typeTranslations[appointmentType] || appointmentType;
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
