<script setup lang="ts">
import { ref, defineProps, defineEmits, watch } from 'vue';
import { type DepartmentDto } from '@/models/Departments/department';

const props = defineProps<{ department: DepartmentDto }>();
const emit = defineEmits(['on-valid-submit']);

const department = ref({ ...props.department });

watch(() => props.department, (newVal) => {
  department.value = { ...newVal };
});

const departmentNameRules = [
  (v: string) => !!v || 'Nazwa oddziału jest wymagana',
];
const numberOfBedsRules = [
  (v: number) => !!v || 'Liczba łóżek jest wymagana',
  (v: number) => v >= 0 || 'Liczba łóżek nie może być mniejsza niż 0',
];
const actualNumberOfPatientsRules = [
  (v: number) => !!v || 'Aktualna liczba pacjentów jest wymagana',
  (v: number) => v >= 0 || 'Aktualna liczba pacjentów nie może być mniejsza niż 0',
];

const handleSubmit = () => {
  if (department.value.departmentName && department.value.numberOfBeds >= 0 && department.value.actualNumberOfPatients >= 0) {
    emit('on-valid-submit', department.value);
  }
};
</script>

<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      v-model="department.departmentName"
      label="Nazwa oddziału"
      :rules="departmentNameRules"
      required
    ></v-text-field>
    <v-text-field
      v-model="department.numberOfBeds"
      label="Liczba łóżek"
      type="number"
      :rules="numberOfBedsRules"
      required
    ></v-text-field>
    <v-text-field
      v-model="department.actualNumberOfPatients"
      label="Aktualna liczba pacjentów"
      type="number"
      :rules="actualNumberOfPatientsRules"
      required
    ></v-text-field>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zatwierdź" color="green" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>

<style scoped>
.v-input {
  background-color: white;
  color: black;
}
</style>
