<template>
  <v-form @submit.prevent="handleSubmit">
    <v-text-field
      label="Imię"
      v-model="form.patientName"
      :rules="nameRules"
      required
    ></v-text-field>
    <v-text-field
      label="Nazwisko"
      v-model="form.patientSurname"
      :rules="surnameRules"
      required
    ></v-text-field>
    <v-text-field
      label="PESEL"
      v-model="form.peselNumber"
      :rules="peselRules"
      required
    ></v-text-field>
    <v-text-field
      label="Diagnoza wstępna"
      v-model="form.initialDiagnosis"
      :rules="diagnosisRules"
      required
    ></v-text-field>
    <v-select
      v-model="form.departmentId"
      label="Oddział"
      :items="formattedDepartments"
      item-title="text"
      item-value="value"
      :rules="departmentRules"
      required
      no-data-text="Brak oddziałów"
    ></v-select>
    <v-select
      v-model="form.roomId"
      label="Pokój"
      :items="formattedRooms"
      item-title="text"
      item-value="value"
      :rules="roomRules"
      required
    ></v-select>
    <v-checkbox
      v-model="form.isDischarged"
      label="Czy wypisany?"
    ></v-checkbox>
    <v-text-field
      label="Leczenie"
      v-model="form.treatment"
      :rules="treatmentRules"
      required
      no-data-text="Brak pokoi"
    ></v-text-field>
    <v-select
      v-model="form.medicalStaffId"
      label="Lekarz prowadzący"
      :items="formattedMedicalStaff"
      item-title="text"
      item-value="value"
      :rules="medicalStaffRules"
      required
    ></v-select>
    <v-text-field
      label="Adres Pacjenta"
      v-model="form.address"
      :rules="addressRules"
      required
    ></v-text-field>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn type="submit" text="Zatwierdź" color="green" variant="flat"></v-btn>
    </v-card-actions>
  </v-form>
</template>

<script setup lang="ts">
import { ref, computed, watch, defineProps, defineEmits } from 'vue';
import { InputCreatePatient } from '@/models/Patients/patient';
import { useRouter } from 'vue-router';

const props = defineProps({
  patient: Object,
  departments: {
    type: Array,
    default: () => []
  },
  rooms: {
    type: Array,
    default: () => []
  },
  medicalStaff: {
    type: Array,
    default: () => []
  }
});
const emits = defineEmits(['update:patient', 'on-valid-submit']);

const form = ref(new InputCreatePatient());
const router = useRouter();

watch(() => props.patient, (newPatient) => {
  form.value = { ...newPatient };
});

const formattedDepartments = computed(() =>
  props.departments.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  }))
);

const formattedRooms = computed(() =>
  props.rooms.map(room => ({
    text: room.roomNumber,
    value: room.roomId
  }))
);

const formattedMedicalStaff = computed(() =>
  props.medicalStaff.map(staff => ({
    text: staff.text,
    value: staff.value
  }))
);

const handleSubmit = () => {
  emits('on-valid-submit', form.value);
  router.push({ name: 'PatientView'});
};

const nameRules = [
  (v: string) => !!v || 'Imię pacjenta jest wymagane',
];

const surnameRules = [
  (v: string) => !!v || 'Nazwisko pacjenta jest wymagane',
];

const peselRules = [
  (v: string) => !!v || 'PESEL pacjenta jest wymagany',
];

const diagnosisRules = [
  (v: string) => !!v || 'Diagnoza wstępna jest wymagana',
];

const departmentRules = [
  v => !!v || 'Oddział jest wymagany',
];

const roomRules = [
  v => !!v || 'Pokój jest wymagany',
];

const treatmentRules = [
  v => !!v || 'Leczenie jest wymagane',
];
const medicalStaffRules = [
  (v: string) => !!v || 'Personel medyczny jest wymagany',
];
const addressRules = [
  (v: string) => !!v || 'Adres jest wymagany',
];
</script>

<style scoped>
.v-input {
  background-color: white;
  color: black;
}
</style>
