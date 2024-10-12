<template>
  <BasePage title="Zarządzanie operacjami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 25rem"
          >+ Zarejestruj w systemie nową operację</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Uzupełnij szczegóły operacji" rounded="lg">
            <OperationForm
              v-model="operationToAdd"
              :patients="patients"
              :medicalStaff="medicalStaff"
              @on-valid-submit="(operation) => { addOperation(operation); isActive.value = false; }"
            ></OperationForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-container>
      <v-row>
        <v-col v-for="operation in operations" :key="operation.operationId" cols="12" md="6" lg="4">
          <v-card>
            <v-card-title>{{ operation.operationName }}</v-card-title>
            <v-card-text>
              <div>Typ znieczulenia: {{ operation.anesthesiaType }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="blue" text @click="goToDetails(operation.operationId)">Szczegóły</v-btn>
              <v-btn color="red" text @click="deleteOperation(operation.operationId)">Usuń</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12">
          <v-select
            v-model="itemsPerPage"
            :items="itemsPerPageOptions"
            label="Liczba obiektów na stronę"
            @change="getOperations"
          ></v-select>
        </v-col>
      </v-row>

      <v-pagination
        v-model="currentPage"
        :length="Math.ceil(totalOperations / itemsPerPage)"
        @input="getOperations"
        color="blue"
      ></v-pagination>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { useOperationStore } from '@/stores/operationStore';
import { usePatientStore } from '@/stores/patientStore';
import { useMedicalStaffStore } from '@/stores/doctorStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import OperationForm from '@/components/operations/CreateOperationForm.vue';
import {InputCreateOperation } from '@/models/Examinations/operation';

const operationStore = useOperationStore();
const patientStore = usePatientStore();
const medicalStaffStore = useMedicalStaffStore();
const toast = useToast();
const router = useRouter();

const currentPage = ref(1);
const itemsPerPage = ref(10);
const itemsPerPageOptions = ref([5, 10, 20, 50]);

const operations = ref([]);
const totalOperations = ref(0);

const operationToAdd = ref(new InputCreateOperation());

const patients = ref([]);
const medicalStaff = ref([]);

const getOperations = async () => {
  try {
    const paginationParams = {
      pageIndex: currentPage.value,
      pageSize: itemsPerPage.value
    };
    await operationStore.dispatchGetOperations(paginationParams);
    operations.value = operationStore.operations;
    totalOperations.value = operationStore.totalItems;
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania operacji');
  }
};

const addOperation = async (operation) => {
  try {
    await operationStore.dispatchCreateOperation(operation);
    toast.success('Pomyślnie dodano nową operację');
    getOperations();
  } catch (error) {
    toast.error('Wystąpił problem podczas dodawania operacji');
  }
};

const deleteOperation = async (id) => {
  try {
    await operationStore.dispatchDeleteOperation(id);
    toast.success('Pomyślnie usunięto operację');
    getOperations();
  } catch (error) {
    toast.error('Wystąpił problem podczas usuwania operacji');
  }
};

const goToDetails = (id) => {
  router.push(`/operation/${id}`);
};

const getPatients = async () => {
  try {
    const data = await patientStore.dispatchGetPatientsList();
    patients.value = data.map(patient => ({
      text: `${patient.peselNumber}`,
      value: patient.patientId
    }));
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania pacjentów');
  }
};

const getMedicalStaff = async () => {
  try {
    const data = await medicalStaffStore.dispatchGetDoctorsList();
    medicalStaff.value = data.map(staff => ({
      text: `${staff.surname} ${staff.name}`,
      value: staff.doctorId
    }));
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania personelu medycznego');
  }
};

onMounted(() => {
  getOperations();
  getPatients();
  getMedicalStaff();
});

watch([currentPage, itemsPerPage], getOperations);
</script>
