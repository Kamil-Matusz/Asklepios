<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Imię"
      v-model="form.name"
      :rules="nameRules"
      required
    ></v-text-field>
    <v-text-field
      label="Nazwisko"
      v-model="form.surname"
      :rules="surnameRules"
      required
    ></v-text-field>
    <v-text-field
      label="Prywatny numer telefonu"
      v-model="form.privatePhoneNumber"
      :rules="phoneRules"
      required
    ></v-text-field>
    <v-text-field
      label="Numer telefonu w szpitalu"
      v-model="form.hospitalPhoneNumber"
      :rules="phoneRules"
      required
    ></v-text-field>
    <v-text-field
      label="Specjalizacja"
      v-model="form.specialization"
      :rules="specializationRules"
      required
    ></v-text-field>
    <v-text-field
      label="Numer licencji medycznej"
      v-model="form.medicalLicenseNumber"
      :rules="licenseRules"
      required
    ></v-text-field>
    <v-select
      v-model="form.departmentId"
      label="Oddział"
      :items="formattedDepartments"
      item-title="text"
      item-value="value"
      :rules="departmentRules"
      required
    ></v-select>
    <v-select
      v-model="form.userId"
      label="Użytkownik"
      :items="formattedUsers"
      item-title="text"
      item-value="value"
      :rules="userRules"
      required
    ></v-select>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zatwierdź" color="green" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>

<script setup lang="ts">
import { ref, computed, watch, defineProps, defineEmits } from 'vue';
import { InputCreateMedicalStaff } from '@/models/Users/doctor';

const props = defineProps({
  doctor: Object,
  departments: {
    type: Array,
    default: () => []
  },
  users: {
    type: Array,
    default: () => []
  }
});
const emits = defineEmits(['update:doctor', 'on-valid-submit']);

const form = ref(new InputCreateMedicalStaff());

watch(() => props.doctor, (newDoctor) => {
  form.value = { ...newDoctor };
});

const formattedDepartments = computed(() =>
  props.departments.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  }))
);

const formattedUsers = computed(() =>
  props.users.map(user => ({
    text: user.email,
    value: user.userId
  }))
);

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
};

const nameRules = [
  (v: string) => !!v || 'Imię jest wymagane',
];

const surnameRules = [
  (v: string) => !!v || 'Nazwisko jest wymagane',
];

const phoneRules = [
  (v: string) => !!v || 'Numer telefonu jest wymagany',
];

const specializationRules = [
  (v: string) => !!v || 'Specjalizacja jest wymagana',
];

const licenseRules = [
  (v: string) => !!v || 'Numer licencji medycznej jest wymagany',
];

const userRules = [
  v => !!v || 'Użytkownik jest wymagany',
];

const departmentRules = [
  v => !!v || 'Oddział jest wymagany',
];
</script>
