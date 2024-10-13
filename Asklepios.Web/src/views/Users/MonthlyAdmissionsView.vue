<template>
  <v-container class="pa-6" style="background-color: #2c5f5c;">
    <v-row>
      <v-col cols="12">
        <v-card class="mx-auto" elevation="6" style="border-radius: 12px; overflow: hidden;">
          <v-sheet color="#1e3c3a" dark>
            <v-card-title class="white--text">
              Zestawienie przyjętych pacjentów
            </v-card-title>
          </v-sheet>
          <v-data-table
            :headers="headers"
            :items="monthlyAdmissionSummary"
            :loading="isLoading"
            loading-text="Ładowanie... Proszę czekać"
            no-data-text="Brak przyjętych pacjentów"
            class="elevation-1 mt-4"
            dense
            hide-default-footer
            style="border-radius: 8px; overflow: hidden; background-color: #2e4348;"
          >
            <template #item="{ item }">
              <tr>
                <td class="pa-3">
                  <v-icon small class="mr-2">mdi-calendar-month</v-icon>
                  {{ item.admissionMonth }}
                </td>
                <td class="text-center pa-3">
                  <v-icon small class="mr-2">mdi-account-multiple</v-icon>
                  {{ item.patientCount }}
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useDepartmentStatsStore } from '@/stores/statisticsStore';

const store = useDepartmentStatsStore();
const monthlyAdmissionSummary = ref([]);
const isLoading = ref(true);

const headers = [
  { text: 'Month', value: 'admissionMonth', align: 'start' },
  { text: 'Patient Count', value: 'patientCount', align: 'center' }
];

onMounted(async () => {
  try {
    await store.dispatchMonthlyAdmissions();
    const rawData = store.monthlyAdmissionSummary;
    monthlyAdmissionSummary.value = rawData.map((item) => ({
      admissionMonth: formatMonth(item.admissionMonth),
      patientCount: item.patientCount
    }));
  } finally {
    isLoading.value = false;
  }
});

function formatMonth(dateString: string): string {
  const options = { year: 'numeric', month: 'long' };
  return new Date(dateString).toLocaleDateString('pl-PL', options);
}
</script>

<style scoped>
.v-card-title, .v-card-subtitle {
  padding: 16px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

.v-data-table {
  border-radius: 12px;
  overflow: hidden;
}

.v-data-table th {
  background-color: #243236;
  color: #b0bec5;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.v-data-table td {
  background-color: #2e4348;
  color: #eceff1;
}

.v-data-table .v-icon {
  vertical-align: middle;
}
</style>
