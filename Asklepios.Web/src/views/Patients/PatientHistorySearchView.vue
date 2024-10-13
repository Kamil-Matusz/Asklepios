<template>
  <v-container>
    <v-row justify="center" class="mt-5">
      <v-col cols="12" class="text-center">
        <h2>Wyszukiwanie historii pacjenta po numerze PESEL</h2>
      </v-col>
    </v-row>

    <v-row justify="center" class="mt-3">
      <v-col cols="12" sm="8" md="6">
        <v-text-field
          v-model="pesel"
          label="Podaj numer PESEL"
          outlined
          clearable
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="4" md="2">
        <v-btn @click="searchPatientHistory" color="yellow" dark class="ma-4">Szukaj</v-btn>
      </v-col>
    </v-row>

    <v-row justify="center" v-if="patientHistory">
      <v-col cols="12" sm="10" md="8">
        <v-card class="pa-4" color="grey darken-3" dark>
          <v-card-title>
            Pacjent: <strong>{{ patientHistory.patientName }} {{ patientHistory.patientSurname }}</strong>
          </v-card-title>
          <v-card-subtitle>PESEL: <strong>{{ patientHistory.peselNumber }}</strong></v-card-subtitle>

          <v-card-text>
            <v-row v-if="patientHistory.history && patientHistory.history.length > 0" v-for="visit in patientHistory.history" :key="visit.admissionDate">
              <v-col cols="12" class="mb-3">
                <v-card color="grey lighten-4" class="pa-3" outlined>
                  <v-row>
                    <v-col cols="12">
                      <strong>Data Przyjęcia:</strong> {{ formatDate(visit.admissionDate) }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Data Wypisu:</strong> {{ formatDate(visit.dischargeDate) }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Nazwa Operacji:</strong> {{ visit.operationName }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Wynik:</strong> {{ visit.result }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Powikłania:</strong> {{ visit.complications }}
                    </v-col>
                    <v-col cols="12">
                      <strong>Typ Znieczulenia:</strong> {{ visit.anesthesiaType }}
                    </v-col>
                  </v-row>
                </v-card>
              </v-col>
            </v-row>
            <div v-else>
              <v-alert type="info" colored-border>
                Brak historii
              </v-alert>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row justify="center" v-if="notFound">
      <v-col cols="12" sm="10" md="8">
        <v-alert type="error" colored-border>
          Nie znaleziono pacjenta o podanym numerze PESEL.
        </v-alert>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref } from 'vue';
import { usePatientHistoryStore } from '@/stores/patientHistoryStore';

const store = usePatientHistoryStore();

const pesel = ref('');
const patientHistory = ref(null);
const notFound = ref(false);

const searchPatientHistory = async () => {
  notFound.value = false;

  if (pesel.value) {
    try {
      const result = await store.dispatchGetPatientHistoryByPesel(pesel.value);
      if (result) {
        patientHistory.value = result;
      } else {
        patientHistory.value = null;
        notFound.value = true;
      }
    } catch (error) {
      patientHistory.value = null;
      notFound.value = true;
      console.error('Błąd podczas pobierania historii pacjenta:', error);
    }
  }
};

const formatDate = (date) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric' };
  return new Date(date).toLocaleDateString('pl-PL', options);
};
</script>

<style scoped>
.v-card {
  margin-top: 20px;
  border-radius: 15px;
  background-color: transparent;
  box-shadow: none;
}

.v-card-title {
  font-size: 1.25rem;
  font-weight: bold;
  text-align: left;
}

.v-row {
  margin-bottom: 10px;
}

.v-col {
  padding: 5px 0;
  text-align: left;
}

.v-card-subtitle {
  margin-top: -10px;
  font-size: 1rem;
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
