<template>
  <v-container>
    <v-row justify="center" class="mt-5">
      <v-col cols="12" class="text-center">
        <h2>Najblisze wizyty</h2>
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
                      <strong>Imię i nazwisko :</strong> {{ appointment.patientName }} {{ appointment.patientSurname }}
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

                    <v-col cols="12" class="d-flex">
                      <div>
                        <v-btn
                          v-if="!editingStatus[appointment.appointmentId]"
                          @click="toggleEditStatus(appointment.appointmentId)"
                          color="blue"
                          dark
                          class="mr-2">
                          Zmień status
                        </v-btn>

                        <div v-if="editingStatus[appointment.appointmentId]" class="d-flex align-center mr-2">
                          <v-select
                            v-model="appointment.status"
                            :items="statusOptions"
                            label="Status"
                            item-title="text"
                            item-value="value"
                            class="mr-2"
                          ></v-select>
                          <v-btn @click="saveStatus(appointment)" color="green" dark class="mr-2">
                            Zapisz
                          </v-btn>
                          <v-btn @click="cancelStatusChange(appointment)" color="grey" dark>
                            Anuluj
                          </v-btn>
                        </div>
                      </div>

                      <v-btn @click="deleteAppointment(appointment.appointmentId)" color="red" dark>
                        Usuń
                      </v-btn>
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
          Brak wizyt na dzisiaj.
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

const futureAppointments = async () => {
  try {
    const result = await store.dispatchGetUserFutureAppointments();
    appointments.value = result;
  } catch (error) {
    console.error('Błąd podczas pobierania wizyt:', error);
  }
};

onMounted(() => {
  futureAppointments();
});

const editingStatus = ref<{ [key: string]: boolean }>({});
const originalStatuses = ref<{ [key: string]: string }>({});

const statusOptions = ref([
  { text: 'Odwołaj', value: 'Cancelled' },
]);

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
    await futureAppointments();
    console.log('Pomyślnie anulowano wizytę.');
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
    await futureAppointments();
  } catch (error) {
    console.error('Błąd podczas usuwania wizyty:', error);
  }
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

const formatDate = (date) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
  return new Date(date).toLocaleDateString('pl-PL', options);
};
</script>
