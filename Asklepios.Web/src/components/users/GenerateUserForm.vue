<script setup lang="ts">
import { ref, defineEmits } from 'vue';

export interface GenerateUserAccount {
  email: string;
  role: string;
  isActive: boolean;
}

const emit = defineEmits(['onValidSubmit', 'onInvalidSubmit']);

const user = ref<GenerateUserAccount>({
  email: '',
  role: '',
  isActive: true,
});

const errors = ref({
  email: '',
  role: '',
  isActive: ''
});

const roles = ref([
  { name: 'Administrator', value: 'Admin' },
  { name: 'Lekarz', value: 'Doctor' },
  { name: 'Pielęgniarka', value: 'Nurse' },
  { name: 'Pracownik IT', value: 'IT Employee' },
  { name: 'Brak roli', value: 'No Role' },
]);

const activityStatus = ref([
  { name: 'Aktywne', value: true },
  { name: 'Nieaktywne', value: false },
]);

const validateEmail = (email: string) => {
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailPattern.test(email) ? '' : 'Nieprawidłowy adres email';
};

const validateRole = (role: string) => {
  return role ? '' : 'Rola jest wymagana';
};

const validateIsActive = (isActive: boolean) => {
  return typeof isActive === 'boolean' ? '' : 'Status aktywności jest wymagany';
};

const validateForm = () => {
  errors.value.email = validateEmail(user.value.email);
  errors.value.role = validateRole(user.value.role);
  errors.value.isActive = validateIsActive(user.value.isActive);

  return !errors.value.email && !errors.value.role && !errors.value.isActive;
};

const submit = async () => {
  if (validateForm()) {
    emit('onValidSubmit', user.value);
  } else {
    emit('onInvalidSubmit');
  }
};
</script>

<template>
  <v-form class="pa-4" @submit.prevent="submit">
    <v-text-field
      v-model="user.email"
      :error-messages="[errors.email]"
      label="Email"
      required
      class="mb-2"
    ></v-text-field>

    <v-select
      v-model="user.role"
      :items="roles"
      item-title="name"
      item-value="value"
      :error-messages="[errors.role]"
      label="Rola"
      required
      class="mb-2"
    ></v-select>

    <v-select
      v-model="user.isActive"
      :items="activityStatus"
      item-title="name"
      item-value="value"
      :error-messages="[errors.isActive]"
      label="Status aktywności"
      required
      class="mb-2"
    ></v-select>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zatwierdź" color="primary" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>
