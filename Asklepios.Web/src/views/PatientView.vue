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
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { usePatientStore } from '@/stores/patientStore';
import { useToast } from 'vue-toastification';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';

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
