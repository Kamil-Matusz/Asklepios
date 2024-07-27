<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Imię"
      v-model="form.name"
      required
    ></v-text-field>
    <v-text-field
      label="Nazwisko"
      v-model="form.surname"
      required
    ></v-text-field>
    <v-select
      v-model="form.departmentId"
      label="Oddział"
      :items="formattedDepartments"
      item-title="text"
      item-value="value"
      required
    ></v-select>
    <v-text-field
      label="Uzytkownik"
      v-model="form.userId"
      required
    ></v-text-field>
    <v-btn type="submit" color="primary">Zatwierdź</v-btn>
  </v-form>
</template>

<script setup lang="ts">
import { ref, computed, watch, defineProps, defineEmits } from 'vue';
import { InputCreateNurse } from '@/models/Users/nurse';

const props = defineProps({
  nurse: Object,
  departments: Array
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

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
};
</script>
