<template>
  <BasePage title="Edytuj szczegóły lekarza">
    <div v-if="form.doctorId !== ''" class="doctor-details">
      <h2>Edycja lekarza {{ form.name }} {{ form.surname }}</h2>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field label="Imię" v-model="form.name" required></v-text-field>
        <v-text-field label="Nazwisko" v-model="form.surname" required></v-text-field>
        <v-text-field label="Prywatny numer telefonu" v-model="form.privatePhoneNumber" required></v-text-field>
        <v-text-field label="Szpitalny numer telefonu" v-model="form.hospitalPhoneNumber" required></v-text-field>
        <v-text-field label="Specjalizacja" v-model="form.specialization" required></v-text-field>
        <v-text-field label="Numer licencji medycznej" v-model="form.medicalLicenseNumber" required></v-text-field>
        <v-select
          v-model="form.departmentId"
          label="Oddział"
          :items="formattedDepartments"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-select
          v-model="form.userId"
          label="Użytkownik"
          :items="formattedUsers"
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
import { useMedicalStaffStore } from '@/stores/doctorStore';
import { useUserStore } from '@/stores/userStore';
import { useDepartmentStore } from '@/stores/departmentStore';
import { useToast } from 'vue-toastification';
import { MedicalStaffDto } from '@/models/Users/doctor';

const route = useRoute();
const router = useRouter();
const medicalStaffStore = useMedicalStaffStore();
const userStore = useUserStore();
const departmentStore = useDepartmentStore();
const toast = useToast();

const form = ref<MedicalStaffDto>({
  doctorId: '',
  name: '',
  surname: '',
  privatePhoneNumber: '',
  hospitalPhoneNumber: '',
  specialization: '',
  medicalLicenseNumber: '',
  departmentId: '',
  userId: ''
});

const fetchDoctorDetails = async () => {
  try {
    const doctorId = route.params.id as string;
    await medicalStaffStore.dispatchGetDoctor(doctorId);
    const doctor = medicalStaffStore.doctorDetails;
    if (doctor) {
      form.value = { ...doctor };
    }
  } catch (error) {
    console.error('Błąd podczas pobierania szczegółów lekarza:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów lekarza');
  }
};

const formattedDepartments = computed(() => {
  return medicalStaffStore.departments?.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  })) || [];
});

const formattedUsers = computed(() => {
  return userStore.doctors?.map(doctor => ({
    text: doctor.email,
    value: doctor.userId
  })) || [];
});

const handleSubmit = async () => {
  try {
    if (
      !form.value.name ||
      !form.value.surname ||
      !form.value.privatePhoneNumber ||
      !form.value.hospitalPhoneNumber ||
      !form.value.specialization ||
      !form.value.medicalLicenseNumber ||
      !form.value.departmentId ||
      !form.value.userId
    ) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    await medicalStaffStore.dispatchUpdateDoctor(form.value.doctorId || '', form.value);
    toast.success('Pomyślnie zaktualizowano dane lekarza!');
    router.go(-1);
  } catch (error) {
    console.error('Błąd podczas aktualizacji lekarza:', error);
    toast.error('Wystąpił problem podczas aktualizacji lekarza');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(async () => {
  await medicalStaffStore.dispatchGetDepartmentsAutocomplete();
  await userStore.dispatchGetDoctors();
  await fetchDoctorDetails();
});
</script>

<style scoped>
.doctor-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  color: black;
}

.doctor-details h2 {
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
