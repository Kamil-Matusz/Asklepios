<template>
  <BasePage title="Edycja oddziału">
    <div v-if="form.departmentName !== ''" class="department-details">
      <h2>Edycja oddziału {{ form.departmentName }}</h2>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field
          label="Nazwa oddziału"
          v-model="form.departmentName"
          required
        ></v-text-field>
        <v-text-field
          label="Liczba łóżek"
          v-model="form.numberOfBeds"
          type="number"
          required
        ></v-text-field>
        <v-text-field
          label="Aktualna liczba pacjentów"
          v-model="form.actualNumberOfPatients"
          type="number"
          required
        ></v-text-field>
        <v-btn type="submit" color="primary">Zatwierdź</v-btn>
      </v-form>
      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useDepartmentStore } from '@/stores/departmentStore';
import { useToast } from 'vue-toastification';

const route = useRoute();
const router = useRouter();
const departmentStore = useDepartmentStore();
const toast = useToast();

const form = ref({
  departmentId: '',
  departmentName: '',
  numberOfBeds: 0,
  actualNumberOfPatients: 0
});

const fetchDepartmentDetails = async () => {
  try {
    const departmentId = route.params.id as string;
    await departmentStore.dispatchGetDepartment(departmentId);
    const department = departmentStore.departmentDetails;
    if (department) {
      form.value = { ...department };
    }
  } catch (error) {
    console.error('Error fetching department details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów oddziału');
  }
};

const handleSubmit = async () => {
  try {
    if (!form.value.departmentName || form.value.numberOfBeds === 0) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    await departmentStore.dispatchUpdateDepartment(form.value.departmentId, form.value);
    toast.success('Pomyślnie zaktualizowano dane oddziału!');
    router.go(-1);
  } catch (error) {
    console.error('Error updating department:', error);
    toast.error('Wystąpił problem podczas aktualizacji oddziału');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(async () => {
  await fetchDepartmentDetails();
});
</script>

<style scoped>
.department-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.department-details h2 {
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
