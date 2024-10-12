<template>
  <BasePage title="Edytuj szczegóły pielęgniarki">
    <div v-if="form.nurseId !== ''" class="nurse-details">
      <h2>Edycja pielęgniarki {{ form.name }} {{ form.surname }}</h2>
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
          v-model="form.userId"
          label="Uzytkownik"
          :items="formattedUsers"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-select
          v-model="form.departmentId"
          label="Oddział"
          :items="formattedDepartments"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-btn type="submit" color="primary">Zatwierdź</v-btn>
      </v-form>
      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useNurseStore } from '@/stores/nurseStore';
import { useUserStore } from '@/stores/userStore';
import { useToast } from 'vue-toastification';
import { NurseDto } from '@/models/Users/nurse';

const route = useRoute();
const router = useRouter();
const nurseStore = useNurseStore();
const userStore = useUserStore();
const toast = useToast();

const form = ref<NurseDto>({
  nurseId: '',
  name: '',
  surname: '',
  departmentId: '',
  userId: '',
});

const fetchNurseDetails = async () => {
  try {
    const nurseId = route.params.id as string;
    await nurseStore.dispatchGetNurse(nurseId);
    const nurse = nurseStore.nurseDetails;
    if (nurse) {
      form.value = { ...nurse };
    }
  } catch (error) {
    console.error('Error fetching nurse details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pielęgniarki');
  }
};

const formattedDepartments = computed(() => {
  return nurseStore.departments?.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  })) || [];
});

const formattedUsers = computed(() => {
  return userStore.nurses?.map(nurse => ({
    text: nurse.email,
    value: nurse.userId
  })) || [];
});

const handleSubmit = async () => {
  try {
    console.log('Dane formularza:', form.value);
    if (!form.value.name || !form.value.surname || !form.value.departmentId || !form.value.userId) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    await nurseStore.dispatchUpdateNurse(form.value.nurseId || '', form.value);
    toast.success('Pomyślnie zaktualizowano dane pielęgniarki!');
    router.go(-1);
  } catch (error) {
    console.error('Error updating nurse:', error);
    toast.error('Wystąpił problem podczas aktualizacji pielęgniarki');
  }
};


const goBack = () => {
  router.go(-1);
};

onMounted(async () => {
  await nurseStore.dispatchGetDepartmentsAutocomplete();
  await userStore.dispatchGetNurses();
  await fetchNurseDetails();
});
</script>

<style scoped>
.nurse-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  color: black;
}

.nurse-details h2 {
  margin-bottom: 10px;
}

.btn-custom {
  margin-top: 15px;
  padding: 10px 20px;
  border: 2px solid #4CAF50;
  border-radius: 5px;
  background-color: #4CAF50;
  color: white;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-custom:hover {
  background-color: #45a049;
  border-color: #45a049;
}
</style>
