<script setup lang="ts">
import { ref } from 'vue';
import { useExamResultsStore } from '@/stores/examResultStore';
import { InputCreateExamResult } from '@/models/Examinations/examResult';
import { useToast } from 'vue-toastification';

const examResultsStore = useExamResultsStore();
const toast = useToast();

const form = ref(new InputCreateExamResult());

const examIdRules = [
  (v: number) => !!v || 'Nazwa badania jest wymagana',
];

const patientIdRules = [
  (v: string) => !!v || 'Pacjent jest wymagany',
];

const resultRules = [
  (v: string) => !!v || 'Opis wyniku badania jest wymagany',
];

const handleSubmit = async () => {
  if (form.value.examId && form.value.result && form.value.patientId) {
    try {
      await examResultsStore.dispatchCreateExamResult(form.value);
      toast.success('Exam result created successfully!');
      resetForm();
    } catch (error) {
      toast.error('Failed to create exam result');
    }
  }
};

const resetForm = () => {
  form.value = {
    examResultId: '',
    patientId: '',
    date: new Date(),
    result: '',
    comment: '',
    examId: null,
  };
};

const formattedPatients = computed(() => {
  return examResultsStore.patients?.map(patient => ({
    text: patient.peselNumber,
    value: patient.patientId
  })) || [];
});

const formattedExaminations = computed(() => {
  return examResultsStore.examinations?.map(exam => ({
    text: exam.examName,
    value: exam.examId
  })) || [];
});

onMounted(async () => {
  await examResultsStore.dispatchGetExaminationsList();
  await examResultsStore.dispatchGetPatientsList();
});

</script>

<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8">
        <v-card>
          <v-card-title>
            Uzupełnij informacje o nowym badaniu
          </v-card-title>
          <br/>
          <v-card-text>
            <v-form @submit.prevent="handleSubmit">
              <v-select
                v-model="form.patientId"
                label="Pacjent (dodaj po peselu)"
                :items="formattedPatients"
                item-title="text"
                item-value="value"
                :rules="patientIdRules"
                required
                no-data-text="Brak pacjentów"
                prepend-icon="mdi-account"
              ></v-select>
              <v-select
                v-model="form.examId"
                label="Nazwa badania"
                :items="formattedExaminations"
                item-title="text"
                item-value="value"
                :rules="examIdRules"
                required
                no-data-text="Brak badań"
                prepend-icon="mdi mdi-head-plus"
              ></v-select>
              <v-textarea
                v-model="form.result"
                label="Wynik"
                :rules="resultRules"
                required
                prepend-icon="mdi mdi-head-plus"
              ></v-textarea>
              <v-textarea
                v-model="form.comment"
                label="Komentarz"
                prepend-icon="mdi mdi-comment"
              ></v-textarea>
              <v-text-field
                v-model="form.date"
                label="Data badania"
                type="date"
                prepend-icon="mdi-calendar"
                required
              ></v-text-field>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn type="submit" text="Zatwierdź" color="green" variant="flat"></v-btn>
                <v-btn @click="resetForm" text="Resetuj" color="red" variant="flat"></v-btn>
              </v-card-actions>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.v-container {
  margin-top: 50px;
}

.v-card {
  background-color: white;
  color: black;
}
</style>
