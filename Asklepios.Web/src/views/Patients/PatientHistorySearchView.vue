<template>
  <v-container>
    <!-- Sekcja wyszukiwania -->
    <v-row justify="center" class="mt-5">
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

    <!-- Wyświetlanie wyników -->
    <v-row justify="center" v-if="patientHistory">
      <v-col cols="12" sm="10" md="8">
        <v-card class="pa-4" color="grey darken-3" dark>
          <v-card-title>
            Historia pacjenta: {{ patientHistory.patientName }} {{ patientHistory.patientSurname }}
          </v-card-title>
          <v-card-subtitle>PESEL: {{ patientHistory.peselNumber }}</v-card-subtitle>

          <v-card-text>
            <v-simple-table v-if="patientHistory.history && patientHistory.history.length > 0">
              <thead>
                <tr>
                  <th>Data Przyjęcia</th>
                  <th>Data Wypisu</th>
                  <th>Nazwa Operacji</th>
                  <th>Wynik</th>
                  <th>Powikłania</th>
                  <th>Typ Znieczulenia</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="visit in patientHistory.history" :key="visit.admissionDate">
                  <td>{{ formatDate(visit.admissionDate) }}</td>
                  <td>{{ formatDate(visit.dischargeDate) }}</td>
                  <td>{{ visit.operationName }}</td>
                  <td>{{ visit.result }}</td>
                  <td>{{ visit.complications }}</td>
                  <td>{{ visit.anesthesiaType }}</td>
                </tr>
              </tbody>
            </v-simple-table>
            <div v-else>
              <v-alert type="info" border="left" colored-border>
                Brak historii
              </v-alert>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <!-- Komunikat o braku pacjenta -->
    <v-row justify="center" v-if="notFound">
      <v-col cols="12" sm="10" md="8">
        <v-alert type="error" border="left" colored-border>
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
}
</style>
