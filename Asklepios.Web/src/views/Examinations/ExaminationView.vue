<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useExaminationStore } from '@/stores/examinationStore';
import { useToast } from 'vue-toastification';
import { type ExaminationDto, type Examination } from '@/models/Examinations/examination';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';
import ExaminationForm from '@/components/examinations/CreateExaminationForm.vue';

const examinationStore = useExaminationStore();
const toast = useToast();
const router = useRouter();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Nazwa badania', key: 'examName', align: 'start' },
  { title: 'Kategoria', key: 'examCategory', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const examinationToAdd = ref<ExaminationDto>({
  examId: 0,
  examName: '',
  examCategory: ''
});

const getExaminations = async () => {
  options.value.loading = true;
  try {
    console.log('Fetching examinations with pagination:', pagination.value);
    await examinationStore.dispatchGetExaminations({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = examinationStore.totalItems;
    console.log('Fetched examinations:', examinationStore.examinations);
  } catch (error) {
    console.error('Error fetching examinations:', error);
    toast.error('Wystąpił problem podczas pobierania badań');
  } finally {
    options.value.loading = false;
  }
};

const addExamination = async (examination: ExaminationDto) => {
  try {
    if (!examination.examName || !examination.examCategory) {
      console.error('Wymagane są wszystkie pola: Nazwa, Kategoria i Opis');
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    console.log('Dodawanie badania z danymi:', examination);
    await examinationStore.dispatchCreateExamination(examination);
    toast.success('Pomyślnie dodano nowe badanie!');
    examinationToAdd.value = { examId: 0, examName: '', examCategory: '' };
    getExaminations();
  } catch (error) {
    console.error('Error adding examination:', error);
    toast.error('Wystąpił problem podczas dodawania badania');
  }
};

const deleteExamination = async (id: number) => {
  await examinationStore.dispatchDeleteExamination(id);
  toast.success('Pomyślnie usunięto badanie!');
  getExaminations();
};

const editExamination = (id: number) => {
  router.push({ name: 'ExaminationEdit', params: { id } });
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getExaminations();
};

onMounted(getExaminations);
</script>

<template>
  <BasePage title="Zarządzanie badaniami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="green"
            variant="flat"
            class="mb-4"
            style="max-width: 25rem">
            + Dołącz szczegóły nowego badania
        </v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowe badanie" rounded="lg">
            <ExaminationForm
              v-model="examinationToAdd"
              @on-valid-submit="(examination) => { addExamination(examination); isActive.value = false; }"
            ></ExaminationForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="examinationStore.examinations"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="examId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-pencil"
          @click="() => editExamination(item.examId)"
        ></v-btn>
        <v-btn
          rounded="lg"
          size="small"
          color="red"
          class="ml-2"
          icon="mdi-delete"
          @click="() => deleteExamination(item.examId)"
        ></v-btn>
      </template>

      <template #item.examName="{ item }">
        {{ item.examName }}
      </template>

      <template #item.examCategory="{ item }">
        {{ item.examCategory }}
      </template>
    </v-data-table-server>
  </BasePage>
</template>
