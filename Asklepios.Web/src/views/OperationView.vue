<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useOperationStore } from '@/stores/operationStore';
import { useToast } from 'vue-toastification';
import { type OperationDto} from '@/models/Examinations/operation';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import { useRouter } from 'vue-router';

const operationStore = useOperationStore();
const toast = useToast();
const router = useRouter();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Nazwa operacji', key: 'operationName', align: 'start' },
  { title: 'Typ znieczulenia', key: 'anesthesiaType', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const operationToAdd = ref<OperationDto>({
  operationId: '',
  operationName: '',
  startDate: new Date(),
  endDate: new Date(),
  anesthesiaType: '',
  result: '',
  complications: '',
  patientId: '',
  medicalStaffId: ''
});

const getOperations = async () => {
  options.value.loading = true;
  try {
    console.log('Fetching operations with pagination:', pagination.value);
    await operationStore.dispatchGetOperations({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = operationStore.totalItems;
    console.log('Fetched operations:', operationStore.operations);
  } catch (error) {
    console.error('Error fetching operations:', error);
    toast.error('Wystąpił problem podczas pobierania operacji');
  } finally {
    options.value.loading = false;
  }
};

const addOperation = async (operation: OperationDto) => {
  try {
    if (!operation.operationName || !operation.anesthesiaType) {
      console.error('Wymagane są wszystkie pola: Nazwa operacji i Typ znieczulenia');
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    console.log('Dodawanie operacji z danymi:', operation);
    await operationStore.dispatchCreateOperation(operation);
    toast.success('Pomyślnie dodano nową operację!');
    operationToAdd.value = {
      operationId: '',
      operationName: '',
      startDate: new Date(),
      endDate: new Date(),
      anesthesiaType: '',
      result: '',
      complications: '',
      patientId: '',
      medicalStaffId: ''
    };
    getOperations();
  } catch (error) {
    console.error('Error adding operation:', error);
    toast.error('Wystąpił problem podczas dodawania operacji');
  }
};

const deleteOperation = async (id: string) => {
  await operationStore.dispatchDeleteOperation(id);
  toast.success('Pomyślnie usunięto operację!');
  getOperations();
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getOperations();
};

const goToOperationDetails = (operationId: string) => {
  router.push(`/operation/${operationId}`);
};

onMounted(getOperations);
</script>

<template>
  <BasePage title="Zarządzanie operacjami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="primary"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem"
          >+Dodaj nową operację</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowa operacja" rounded="lg">
            <OperationForm
              v-model="operationToAdd"
              @on-valid-submit="(operation) => { addOperation(operation); isActive.value = false; }"
            ></OperationForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="operationStore.operations"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="operationId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          rounded="lg"
          size="small"
          color="red"
          class="ml-2"
          icon="mdi-delete"
          @click="() => deleteOperation(item.operationId)"
        ></v-btn>
        <v-btn
          rounded="lg"
          size="small"
          color="blue"
          class="ml-2"
          icon="mdi-eye"
          @click="() => goToOperationDetails(item.operationId)"
        ></v-btn>
      </template>

      <template #item.operationName="{ item }">
        {{ item.operationName }}
      </template>

      <template #item.anesthesiaType="{ item }">
        {{ item.anesthesiaType }}
      </template>
    </v-data-table-server>
  </BasePage>
</template>

