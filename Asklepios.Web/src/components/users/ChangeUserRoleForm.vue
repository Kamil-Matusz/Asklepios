<script setup lang="ts">
import { ref, defineEmits, defineProps, PropType } from 'vue';

const emit = defineEmits(['onValidSubmit', 'onInvalidSubmit']);

const props = defineProps({
  user: {
    type: Object as PropType<{ userId: string, email: string, role: string }>,
    required: true,
  },
});

const user = ref({ ...props.user });

const roles = ref([
  { name: 'Administrator', value: 'Admin' },
  { name: 'Lekarz', value: 'Doctor' },
  { name: 'Pielęgniarka', value: 'Nurse' },
  { name: 'Pracownik IT', value: 'IT Employee' },
  { name: 'Brak roli', value: 'No Role' },
]);

const errors = ref({
  role: '',
});

const validateRole = (role: string) => {
  return role ? '' : 'Rola jest wymagana';
};

const validateForm = () => {
  errors.value.role = validateRole(user.value.role);
  return !errors.value.role;
};

const submit = () => {
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
      label="Email"
      disabled
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

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zatwierdź zmiany" color="green" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>

<style scoped>
.v-input {
  background-color: white;
  color: black;
}

.v-form {
  background-color: white;
  color: black;
}
</style>
