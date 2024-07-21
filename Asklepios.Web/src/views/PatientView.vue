<template>
  <BasePage title="Zarządzanie pacjentami">
    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="patientStore.patients"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="patientId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="fetchPatientDetails(item.patientId)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-eye"
        ></v-btn>
        <v-dialog max-width="500">
          <template #activator="{ props: activatorProps }">
            <v-btn
              rounded="lg"
              size="small"
              color="red"
              v-bind="activatorProps"
              class="ml-2"
              icon="mdi-delete"
            ></v-btn>
          </template>
          <template #default="{ isActive }">
            <v-card title="Potwierdź swoją decyzję">
              <v-card-text>
                Czy na pewno chcesz usunąć tego pacjenta?
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  text="Usuń pacjenta"
                  color="red"
                  rounded="lg"
                  @click="() => { isActive.value = false; deletePatient(item.patientId); }"
                ></v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </v-dialog>
      </template>
    </v-data-table-server>

    <v-dialog v-model="isDetailsDialogActive" max-width="800">
      <template #default>
        <v-card>
          <v-card-title>Szczegóły pacjenta</v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" md="6">
                  <div><strong>Imię:</strong> {{ patientDetails?.patientName }}</div>
                  <div><strong>Nazwisko:</strong> {{ patientDetails?.patientSurname }}</div>
                  <div><strong>Pesel:</strong> {{ patientDetails?.peselNumber }}</div>
                  <div><strong>Diagnoza wstępna:</strong> {{ patientDetails?.initialDiagnosis }}</div>
                </v-col>
                <v-col cols="12" md="6">
                  <div><strong>Leczenie:</strong> {{ patientDetails?.treatment }}</div>
                  <div><strong>Czy wypisany:</strong> {{ patientDetails?.isDischarged ? 'Tak' : 'Nie' }}</div>
                </v-col>
              </v-row>
              <v-divider class="my-4"></v-divider>
              <v-expansion-panels>
                <v-expansion-panel>
                  <v-expansion-panel-header>Operacje</v-expansion-panel-header>
                  <v-expansion-panel-content>
                    <div v-if="patientDetails?.operations.length">
                      <v-container>
                        <v-row v-for="operation in patientDetails?.operations" :key="operation.operationId">
                          <v-col cols="12" md="6"><strong>Nazwa:</strong> {{ operation.operationName }}</v-col>
                        </v-row>
                        <v-row v-for="operation in patientDetails?.operations" :key="operation.operationId">
                          <v-col cols="12" md="6"><strong>Data rozpoczęcia:</strong> {{ operation.startDate }}</v-col>
                        </v-row>
                        <v-row v-for="operation in patientDetails?.operations" :key="operation.operationId">
                          <v-col cols="12" md="6"><strong>Typ znieczulenia:</strong> {{ operation.anesthesiaType }}</v-col>
                        </v-row>
                        <v-row v-for="operation in patientDetails?.operations" :key="operation.operationId">
                          <v-col cols="12" md="6"><strong>Wynik operacji:</strong> {{ operation.result }}</v-col>
                        </v-row>
                        <v-row v-for="operation in patientDetails?.operations" :key="operation.operationId">
                          <v-col cols="12" md="6"><strong>Kompilacje:</strong> {{ operation.complications }}</v-col>
                        </v-row>
                      </v-container>
                    </div>
                    <div v-else>
                      Brak operacji
                    </div>
                  </v-expansion-panel-content>
                </v-expansion-panel>
                <v-expansion-panel>
                  <v-expansion-panel-header>Wyniki badań</v-expansion-panel-header>
                  <v-expansion-panel-content>
                    <div v-if="patientDetails?.examResults.length">
                      <v-container>
                        <v-row v-for="exam in patientDetails?.examResults" :key="exam.examResultId">
                          <v-col cols="12" md="6"><strong>Nazwa badania:</strong> {{ exam.examName }}</v-col>
                          <v-col cols="12" md="6"><strong>Wynik:</strong> {{ exam.result }}</v-col>
                        </v-row>
                      </v-container>
                    </div>
                    <div v-else>
                      Brak wyników badań
                    </div>
                  </v-expansion-panel-content>
                </v-expansion-panel>
              </v-expansion-panels>
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
import { usePatientStore } from '@/stores/patientStore';
import { useToast } from 'vue-toastification';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import { type PatientDetailsDto} from '@/models/Patients/patient';

const patientStore = usePatientStore();
const toast = useToast();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Imię', key: 'patientName', align: 'start' },
  { title: 'Nazwisko', key: 'patientSurname', align: 'start' },
  { title: 'Oddział', key: 'departmentName', align: 'start' },
  { title: 'Pokój', key: 'roomNumber', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const isDetailsDialogActive = ref(false);
const patientDetails = ref<PatientDetailsDto | null>(null);

const getPatients = async () => {
  options.value.loading = true;
  try {
    await patientStore.dispatchGetPatients({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = patientStore.totalItems;
  } catch (error) {
    console.error('Error fetching patients:', error);
    toast.error('Wystąpił problem podczas pobierania pacjentów');
  } finally {
    options.value.loading = false;
  }
};

const fetchPatientDetails = async (id: string) => {
  try {
    patientDetails.value = await patientStore.dispatchGetPatient(id);
    isDetailsDialogActive.value = true;
  } catch (error) {
    console.error('Error fetching patient details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pacjenta');
  }
};

const deletePatient = async (id: string) => {
  await patientStore.dispatchDeletePatient(id);
  toast.success('Pomyślnie usunięto pacjenta!');
  getPatients();
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getPatients();
};

onMounted(getPatients);
</script>

<style scoped>
/* Add your styles here if needed */
</style>
