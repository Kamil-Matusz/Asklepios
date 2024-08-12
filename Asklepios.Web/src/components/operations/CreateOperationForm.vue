<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Nazwa operacji"
      v-model="form.operationName"
      :rules="operationNameRules"
      required
    ></v-text-field>
    <v-date-picker
      v-model="form.startDate"
      label="Data rozpoczęcia"
      :rules="dateRules"
      required
    ></v-date-picker>
    <v-date-picker
      v-model="form.endDate"
      label="Data zakończenia"
      :rules="dateRules"
      required
    ></v-date-picker>
    <v-select
      v-model="form.anesthesiaType"
      label="Typ znieczulenia"
      :items="anesthesiaTypes"
      item-title="text"
      item-value="value"
      :rules="anesthesiaTypeRules"
      required
></v-select>
    <v-text-field
      label="Wynik"
      v-model="form.result"
      required
    ></v-text-field>
    <v-text-field
      label="Komplikacje"
      v-model="form.complications"
    ></v-text-field>
    <v-select
      v-model="form.patientId"
      label="Pacjent (dodaj po peselu)"
      :items="formattedPatients"
      item-title="text"
      item-value="value"
      :rules="patientRules"
      required
      no-data-text="Brak pacjentów"
    ></v-select>
    <v-select
      v-model="form.medicalStaffId"
      label="Personel medyczny"
      :items="formattedMedicalStaff"
      item-title="text"
      item-value="value"
      :rules="medicalStaffRules"
      required
    ></v-select>
    <v-btn type="submit" color="primary">Zatwierdź</v-btn>
  </v-form>
</template>

<script setup lang="ts">
import { ref, computed, watch, defineProps, defineEmits } from 'vue';
import { InputCreateOperation } from '@/models/Examinations/operation';

const props = defineProps({
  modelValue: Object,
  patients: {
    type: Array,
    default: () => []
  },
  medicalStaff: {
    type: Array,
    default: () => []
  }
});
const emits = defineEmits(['update:modelValue', 'on-valid-submit']);

const form = ref(new InputCreateOperation());

watch(() => props.modelValue, (newOperation) => {
  form.value = { ...newOperation };
});

const formattedPatients = computed(() =>
  props.patients.map(patient => ({
    text: patient.text,
    value: patient.value
  }))
);

const formattedMedicalStaff = computed(() =>
  props.medicalStaff.map(staff => ({
    text: staff.text,
    value: staff.value
  }))
);

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
};

const operationNameRules = [
  (v: string) => !!v || 'Nazwa operacji jest wymagana',
];

const dateRules = [
  (v: string) => !!v || 'Data jest wymagana',
];

const anesthesiaTypeRules = [
  (v: string) => !!v || 'Typ znieczulenia jest wymagany',
];

const patientRules = [
  (v: string) => !!v || 'Pacjent jest wymagany',
];

const medicalStaffRules = [
  (v: string) => !!v || 'Personel medyczny jest wymagany',
];

const anesthesiaTypes = ref([
  { text: 'Ogólne', value: 'Ogólne' },
  { text: 'Miejscowe', value: 'Miejscowe' },
  { text: 'Regionalne', value: 'Regionalne' },
  { text: 'Rdzeniowe', value: 'Rdzeniowe' },
]);
</script>
