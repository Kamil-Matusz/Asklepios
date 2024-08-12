<script setup lang="ts">
import { ref, defineEmits, defineProps, PropType, watch } from 'vue';
import { type InputCreateExamination } from '@/models/Examinations/examination';

const props = defineProps<{
  modelValue: InputCreateExamination
}>();

const emit = defineEmits(['update:modelValue', 'on-valid-submit']);

const localExamination = ref({ ...props.modelValue });

watch(
  () => props.modelValue,
  (newExamination) => {
    localExamination.value = { ...newExamination };
  }
);

const categories = ref([
  { text: 'Laboratoryjne', value: 'Laboratoryjne' },
  { text: 'Obrazowe', value: 'Obrazowe' },
  { text: 'Funkcjonalne', value: 'Funkcjonalne' }
]);

const errors = ref({
  examName: '',
  examCategory: ''
});

const validateName = (name: string) => {
  return name ? '' : 'Nazwa badania jest wymagana';
};

const validateCategory = (category: string) => {
  return category ? '' : 'Kategoria jest wymagana';
};

const validateForm = () => {
  errors.value.examName = validateName(localExamination.value.examName);
  errors.value.examCategory = validateCategory(localExamination.value.examCategory);
  return !errors.value.examName && !errors.value.examCategory;
};

const submitForm = () => {
  if (validateForm()) {
    emit('on-valid-submit', localExamination.value);
  } else {
    alert('Wszystkie pola muszą być wypełnione');
  }
};

const handleInputChange = (key: keyof InputCreateExamination, value: string) => {
  localExamination.value[key] = value;
  emit('update:modelValue', localExamination.value);
};
</script>

<template>
  <v-form @submit.prevent="submitForm">
    <v-text-field
      v-model="localExamination.examName"
      label="Nazwa badania"
      :error-messages="[errors.examName]"
      required
      @input="handleInputChange('examName', $event.target.value)"
      class="mb-2"
    ></v-text-field>

    <v-select
      v-model="localExamination.examCategory"
      :items="categories"
      item-title="text"
      item-value="value"
      :error-messages="[errors.examCategory]"
      label="Kategoria"
      required
      class="mb-2"
      @change="handleInputChange('examCategory', $event)"
    ></v-select>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" color="green">Dodaj badanie</v-btn>
    </v-card-actions>
  </v-form>
</template>

<style scoped>
.v-card-actions {
  display: flex;
  justify-content: flex-end;
}
</style>
