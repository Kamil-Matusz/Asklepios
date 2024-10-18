<template>
  <BasePage title="Zarządzanie pacjentami przychodni">
    <v-data-table-server
      class="custom-table-background"
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="clinicPatientStore.patients"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="clinicPatientId"
      @update:options="handlePagination">
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="fetchClinicPatientDetails(item.clinicPatientId)"
          rounded="lg"
          size="small"
          color="yellow"
          class="ml-2"
          icon="mdi-eye"
        ></v-btn>
        <v-btn
          rounded="lg"
          size="small"
          color="red"
          class="ml-2"
          icon="mdi-delete"
          @click="deleteClinicPatient(item.clinicPatientId)"
        ></v-btn>
        <v-btn
          @click="() => redirectToEditPatientPage(item.clinicPatientId)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-pen"
        ></v-btn>
      </template>
    </v-data-table-server>
    <v-dialog v-model="isDetailsDialogActive" max-width="500">
      <v-card>
        <v-card-title>Szczegóły pacjenta</v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <div><strong>Imię:</strong> {{ clinicPatientDetails?.patientName }}</div>
                <div><strong>Nazwisko:</strong> {{ clinicPatientDetails?.patientSurname }}</div>
                <div><strong>Pesel:</strong> {{ clinicPatientDetails?.peselNumber }}</div>
                <div><strong>Numer telefonu:</strong> {{ clinicPatientDetails?.contactNumber }}</div>
                <div><strong>Email:</strong> {{ clinicPatientDetails?.email }}</div>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text="Zamknij" color="primary" rounded="lg" @click="isDetailsDialogActive = false"></v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useToast } from 'vue-toastification';
import { useClinicPatientsStore } from '@/stores/clinicPatientStore';
import BasePage from '@/components/pages/BasePage.vue';
import { useRouter } from 'vue-router';

const clinicPatientStore = useClinicPatientsStore();
const toast = useToast();
const router = useRouter();

const pagination = ref({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Imię', key: 'patientName', align: 'start' },
  { title: 'Nazwisko', key: 'patientSurname', align: 'start' },
  { title: 'Numer telefonu', key: 'contactNumber', align: 'start' },
  { title: 'Adres email', key: 'email', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const isDetailsDialogActive = ref(false);
const showCreatePatientDialog = ref(false);
const clinicPatientDetails = ref(null);

const getClinicPatients = async () => {
  options.value.loading = true;
  try {
    await clinicPatientStore.dispatchGetAllClinicPatients(
      pagination.value.PageIndex,
      pagination.value.PageSize
    );
    options.value.totalItems = clinicPatientStore.patients.length;
  } catch (error) {
    console.error('Error fetching clinic patients:', error);
    toast.error('Wystąpił problem podczas pobierania pacjentów przychodni');
  } finally {
    options.value.loading = false;
  }
};

const fetchClinicPatientDetails = async (id: string) => {
  try {
    clinicPatientDetails.value = await clinicPatientStore.dispatchGetClinicPatient(id);
    isDetailsDialogActive.value = true;
  } catch (error) {
    console.error('Error fetching clinic patient details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pacjenta');
  }
};

const deleteClinicPatient = async (id: string) => {
  try {
    await clinicPatientStore.dispatchDeleteClinicPatient(id);
    toast.success('Pomyślnie usunięto pacjenta!');
    getClinicPatients();
  } catch (error) {
    console.error('Error deleting clinic patient:', error);
    toast.error('Wystąpił problem podczas usuwania pacjenta');
  }
};

const handlePagination = ({ page, itemsPerPage }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getClinicPatients();
};

const redirectToEditPatientPage = (id: string) => {
  router.push({ name: 'ClinicPatientEdit', params: { id } });
};

onMounted(() => {
  getClinicPatients();
});
</script>

<style scoped>
.custom-table-background {
  background-color: gainsboro;
  color: black;
}

.v-card {
  background-color: white;
  color: black;
}
</style>
