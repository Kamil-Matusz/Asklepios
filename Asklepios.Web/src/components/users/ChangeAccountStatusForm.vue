<script setup lang="ts">
import { ref, defineEmits, defineProps, PropType, watch } from 'vue';

const emit = defineEmits(['onValidSubmit', 'onInvalidSubmit']);

const props = defineProps({
  user: {
    type: Object as PropType<{ userId: string, isActive: boolean }>,
    required: true,
  },
});

const user = ref({ ...props.user });

const statuses = ref([
  { text: 'Aktywne', value: true },
  { text: 'Nieaktywne', value: false },
]);

const errors = ref({
  status: '',
});

const validateStatus = (status: boolean) => {
  return typeof status === 'boolean' ? '' : 'Status jest wymagany';
};

const validateForm = () => {
  errors.value.status = validateStatus(user.value.isActive);
  return !errors.value.status;
};

const submit = () => {
  if (validateForm()) {
    emit('onValidSubmit', user.value);
  } else {
    emit('onInvalidSubmit');
  }
};

watch(
  () => props.user,
  (newUser) => {
    user.value = { ...newUser };
  }
);
</script>

<template>
  <v-form class="pa-4" @submit.prevent="submit">
    <v-text-field
      v-model="user.userId"
      label="ID Użytkownika"
      disabled
      class="mb-2"
    ></v-text-field>

    <v-select
      v-model="user.isActive"
      :items="statuses"
      item-title="text"
      item-value="value"
      :error-messages="[errors.status]"
      label="Status konta"
      required
      class="mb-2"
    ></v-select>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zapisz zmiany" color="green" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>
