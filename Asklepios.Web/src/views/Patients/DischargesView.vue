<template>
  <BasePage title="Spis pacjentów wypisanych">
    <v-data-table-server
      class="custom-table-background"
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="dischargeStore.discharges"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="dischargeId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="fetchDischargeDetails(item.dischargeId)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-eye"
        ></v-btn>
        <v-btn
          :to="{ name: 'DischargeTemplate', params: { id: item.dischargeId } }"
          rounded="lg"
          size="small"
          color="green"
          class="ml-2"
          icon="mdi-file"
        ></v-btn>
      </template>
    </v-data-table-server>

    <v-dialog v-model="isDetailsDialogActive" max-width="800">
      <template #default>
        <v-card>
          <v-card-title>Szczegóły wypisu</v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" md="6">
                  <div><strong>Imię pacjenta:</strong> {{ dischargeDetails?.patientName }}</div>
                  <div><strong>Nazwisko pacjenta:</strong> {{ dischargeDetails?.patientSurname }}</div>
                  <div><strong>Pesel:</strong> {{ dischargeDetails?.peselNumber }}</div>
                  <div><strong>Adres:</strong> {{ dischargeDetails?.address }}</div>
                  <div><strong>Data wypisu:</strong> {{ dischargeDetails?.date }}</div>
                </v-col>
                <v-col cols="12" md="6">
                  <div><strong>Powód wypisu:</strong> {{ dischargeDetails?.dischargeReasson }}</div>
                  <div><strong>Podsumowanie:</strong> {{ dischargeDetails?.summary }}</div>
                  <div><strong>Imię lekarza:</strong> {{ dischargeDetails?.doctorName }}</div>
                  <div><strong>Nazwisko lekarza:</strong> {{ dischargeDetails?.doctorSurname }}</div>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text="Zamknij" color="primary" rounded="lg" @click="isDetailsDialogActive = false"></v-btn>
          </v-card-actions>
        </v-card>
      </template>
    </v-dialog>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useToast } from 'vue-toastification';
import BasePage from '@/components/pages/BasePage.vue';
import { useDischargeStore } from '@/stores/dischargeStore';
import { type DischargeDetailsDto } from '@/models/Patients/discharge';
import { useRouter } from 'vue-router';

const dischargeStore = useDischargeStore();
const toast = useToast();
const router = useRouter();

const headers = [
  { title: 'Imię pacjenta', key: 'patientName', align: 'start' },
  { title: 'Nazwisko pacjenta', key: 'patientSurname', align: 'start' },
  { title: 'Pesel', key: 'peselNumber', align: 'start' },
  { title: 'Adres', key: 'address', align: 'start' },
  { title: 'Data wypisu', key: 'date', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: 10,
  loading: true,
  totalItems: 0
});

const isDetailsDialogActive = ref(false);
const dischargeDetails = ref<DischargeDetailsDto | null>(null);

const getDischarges = async () => {
  options.value.loading = true;
  try {
    await dischargeStore.dispatchGetAllDischarges();
    options.value.totalItems = dischargeStore.totalItems;
  } catch (error) {
    console.error('Error fetching discharges:', error);
    toast.error('Wystąpił problem podczas pobierania wypisów');
  } finally {
    options.value.loading = false;
  }
};

const fetchDischargeDetails = async (id: string) => {
  try {
    dischargeDetails.value = await dischargeStore.dispatchGetDischarge(id);
    isDetailsDialogActive.value = true;
  } catch (error) {
    console.error('Error fetching discharge details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów wypisu');
  }
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  options.value.itemsPerPage = itemsPerPage;
  getDischarges();
};

onMounted(() => {
  getDischarges();
});
</script>

<style scoped>
.custom-table-background {
  background-color: gainsboro;
  color: black;
}
</style>
