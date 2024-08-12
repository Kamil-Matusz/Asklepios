<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Numer pokoju"
      v-model="form.roomNumber"
      type="number"
      required
    ></v-text-field>
    <v-select
      v-model="form.roomType"
      :items="roomTypes"
      item-title="text"
      item-value="value"
      label="Typ pokoju"
      required
    ></v-select>
    <v-text-field
      label="Liczba łóżek"
      v-model="form.numberOfBeds"
      type="number"
      required
    ></v-text-field>
    <v-select
      v-model="form.departmentId"
      label="Oddział"
      class="ml-4 mr-4"
      variant="outlined"
      :items="formattedDepartments"
      item-title="text"
      item-value="value"
      required
    ></v-select>
    <v-btn type="submit" color="green">Zatwierdź</v-btn>
  </v-form>
</template>

<script setup lang="ts">
import { ref, computed, watch, defineProps, defineEmits } from 'vue';

const props = defineProps({
  room: Object,
  departments: Array
});
const emits = defineEmits(['update:room', 'on-valid-submit']);

const roomTypes = ref([
  { text: 'Sala Ogólna', value: 'Sala Ogólna' },
  { text: 'Sala Operacyjna', value: 'Sala Operacyjna' },
  { text: 'Izolatka', value: 'Izolatka' },
]);

const form = ref({ ...props.room });

watch(() => props.room, (newRoom) => {
  form.value = { ...newRoom };
});

const formattedDepartments = computed(() =>
  props.departments.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  }))
);

const updateDepartment = (value: string) => {
  form.value.departmentId = value;
  emits('update:room', form.value);
};

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
};
</script>
