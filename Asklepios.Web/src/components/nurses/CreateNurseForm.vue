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
import { InputCreateNurse } from '@/models/Users/nurse';

const props = defineProps({
  nurse: Object,
  departments: {
    type: Array,
    default: () => []
  },
  users: {
    type: Array,
    default: () => []
  }
});
const emits = defineEmits(['update:nurse', 'on-valid-submit']);

const form = ref(new InputCreateNurse());

watch(() => props.nurse, (newNurse) => {
  form.value = { ...newNurse };
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
  (v: string) => !!v || 'Imie pielęgnarki jest wymagane',
];

const surnameRules = [
  (v: string) => !!v || 'Nazwisko pielęgnarki jest wymagane',
];

const userRules = [
  v => !!v || 'Użytkownik jest wymagany',
];

const departmentRules = [
  v => !!v || 'Oddział jest wymagany',
];
</script>

<style scoped>
.v-input {
  background-color: white;
  color: black;
}
</style>
