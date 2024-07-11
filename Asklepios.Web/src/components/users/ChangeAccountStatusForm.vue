<template>
  <v-form @submit.prevent="submitForm">
    <v-row>
      <v-col cols="12">
        <v-select
          v-model="status"
          :items="statusOptions"
          label="Status konta"
          required
        ></v-select>
      </v-col>
    </v-row>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" color="primary">Zapisz</v-btn>
    </v-card-actions>
  </v-form>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits, watch } from 'vue';

const props = defineProps<{ user: { isActive: boolean } }>();
const emit = defineEmits(['onValidSubmit', 'onInvalidSubmit']);

const status = ref(props.user.isActive);
const statusOptions = ref([
  { text: 'Aktywne', value: true },
  { text: 'Nieaktywne', value: false }
]);

watch(() => props.user, (newVal) => {
  status.value = newVal.isActive;
});

const submitForm = () => {
  if (status.value !== null) {
    emit('onValidSubmit', { userId: props.user.userId, status: status.value });
  } else {
    emit('onInvalidSubmit');
  }
};
</script>
