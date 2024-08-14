<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useExaminationStore } from '@/stores/examinationStore';
import { useToast } from 'vue-toastification';
import { type ExaminationDto } from '@/models/Examinations/examination';
import BasePage from '@/components/pages/BasePage.vue';

const examinationStore = useExaminationStore();
const toast = useToast();
const route = useRoute();
const router = useRouter();

const examinationId = ref(parseInt(route.params.id as string, 10));
const examination = ref<ExaminationDto>({
  examId: 0,
  examName: '',
  examCategory: ''
});

const categories = ref([
  { text: 'Laboratoryjne', value: 'Laboratoryjne' },
  { text: 'Obrazowe', value: 'Obrazowe' },
  { text: 'Funkcjonalne', value: 'Funkcjonalne' }
]);

const getExamination = async () => {
  try {
    const data = await examinationStore.dispatchGetExamination(examinationId.value);
    examination.value = {
      examId: data.examId,
      examName: data.examName,
      examCategory: data.examCategory
    };
  } catch (error) {
    console.error('Error fetching examination:', error);
    toast.error('Wystąpił problem podczas pobierania badania');
  }
};

const updateExamination = async () => {
  try {
    await examinationStore.dispatchUpdateExamination(examinationId.value, examination.value);
    toast.success('Pomyślnie zaktualizowano badanie!');
    router.push({ name: 'examinations' });
  } catch (error) {
    console.error('Error updating examination:', error);
    toast.error('Wystąpił problem podczas aktualizowania badania');
  }
};

const cancelUpdate = () => {
  router.push({ name: 'examinations' });
};

onMounted(getExamination);
</script>

<template>
  <BasePage title="Edycja badania">
    <v-form @submit.prevent="updateExamination">
      <v-text-field
        v-model="examination.examName"
        label="Nazwa badania"
        required
      ></v-text-field>
      <v-select
        v-model="examination.examCategory"
        :items="categories"
        item-title="text"
        item-value="value"
        label="Kategoria"
        required
      ></v-select>
      <v-btn type="submit" color="primary">ZATWIERDŹ</v-btn>
      <br/>
      <br/>
      <v-btn @click="cancelUpdate" color="green">Powrót</v-btn>
    </v-form>
  </BasePage>
</template>
