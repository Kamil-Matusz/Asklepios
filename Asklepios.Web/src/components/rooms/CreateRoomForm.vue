<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Numer pokoju"
      v-model="form.roomNumber"
      type="number"
      required
    ></v-text-field>
    <v-text-field
      label="Typ pokoju"
      v-model="form.roomType"
      required
    ></v-text-field>
    <v-text-field
      label="Liczba łóżek"
      v-model="form.numberOfBeds"
      type="number"
      required
    ></v-text-field>
    <v-autocomplete
      label="Oddział"
      v-model="form.departmentId"
      :items="departments"
      item-text="departmentName"
      item-value="departmentId"
      required
    ></v-autocomplete>
    <v-btn type="submit" color="primary">Zatwierdź</v-btn>
  </v-form>
</template>

<script setup>
import { ref, watch, defineProps, defineEmits } from 'vue';

const props = defineProps({
  room: Object,
  departments: Array
});
const emits = defineEmits(['update:room', 'on-valid-submit']);

const form = ref({ ...props.room });

watch(() => props.room, (newRoom) => {
  form.value = { ...newRoom };
});

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
};
</script>

<style scoped>
/* Add any scoped styles if necessary */
</style>
