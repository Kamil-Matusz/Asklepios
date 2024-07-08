<script setup lang="ts">
import useVuelidate from '@vuelidate/core';
import { userRules } from '../../validations/userRules';
import { GenerateUserAccount } from '../../models/Users/user';
import ValidatedTextField from '../ui/ValidatedTextField.vue';
import { ref } from 'vue';

const emit = defineEmits(['onValidSubmit', 'onInvalidSubmit']);
const user = ref<GenerateUserAccount>({ role: '', email: '', isActive: true });
const validator = useVuelidate(userRules, user);

const roles = ref([
    { name: 'Administrator', value: 'Admin' },
    { name: 'Lekarz', value: 'Doctor' },
    { name: 'Pielęgniarka', value: 'Nurse' },
    { name: 'Pracownik IT', value: 'IT Employee' },
    { name: 'Brak roli', value: 'No Role' },
]);

const activityStatus = ref([
    { name: 'Aktywne', value: true },
    { name: 'Nieaktywne', value: false }
]);

const submit = async () => {
    console.log('submit');
    validator.value.$touch();
    const result = await validator.value.$validate();
    if (result) {
        console.log(user.value);
        emit('onValidSubmit', user.value);
        return;
    }
    emit('onInvalidSubmit');
};
</script>


<template>
  <v-form class="pa-4" @submit.prevent="submit">
      <validated-text-field
          v-model="user.value.email"
          :validation-prop="validator.value.email"
          label="Email"
          class="mb-2"
      ></validated-text-field>

      <v-select
          v-model="user.value.role"
          variant="outlined"
          label="Rola"
          class="mb-2"
          :items="roles"
          item-title="name"
          item-value="value"
      ></v-select>

      <v-select
          v-model="user.value.isActive"
          variant="outlined"
          label="Status aktywności"
          class="mb-2"
          :items="activityStatus"
          item-title="name"
          item-value="value"
      ></v-select>

      <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn type="submit" text="Zatwierdź" color="primary" variant="flat"></v-btn>
      </v-card-actions>
  </v-form>
</template>
