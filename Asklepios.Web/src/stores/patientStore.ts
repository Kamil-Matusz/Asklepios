import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type PatientDto, type PatientDetailsDto, type PatientListDto, PatientAutocompleteDto } from '@/models/Patients/patient';
import { type PaginationParams } from '@/models/paginationParams';
import { DepartmentAutocompleteDto } from '@/models/Departments/department';
import { RoomAutocompleteDto } from '@/models/Departments/room';

export const usePatientStore = defineStore('patientsStore', () => {
  const patients = ref<PatientListDto[]>([]);
  const totalItems = ref(0);
  const patientDetails = ref<PatientDetailsDto | null>(null);
  const departments = ref<DepartmentAutocompleteDto[]>([]);
  const rooms = ref<RoomAutocompleteDto[]>([]);

  function addNewPatient(patient: PatientDto) {
    patients.value.push(patient);
  }

  function removePatient(id: string): boolean {
    const idx = patients.value.findIndex((p) => p.patientId === id);
    if (idx === -1) return false;
    patients.value.splice(idx, 1);
    return true;
  }

  function updatePatientDetails(updatedPatient: PatientDetailsDto): boolean {
    const existingPatientIndex = patients.value.findIndex((p) => p.patientId === updatedPatient.patientId);

    if (existingPatientIndex !== -1) {
      patients.value[existingPatientIndex] = updatedPatient;
      return true;
    }
    return false;
  }

  async function dispatchGetPatients(pagination: PaginationParams) {
    const { data } = await API.patients.getAllPatients(pagination);
    patients.value = data;
    totalItems.value = data.length;
  }

  async function dispatchGetDoctorPatients(pagination: PaginationParams) {
    const { data } = await API.patients.getAllPatientsByDoctor(pagination);
    patients.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreatePatient(patient: PatientDto) {
    await API.patients.createPatient(patient);
    addNewPatient(patient);
  }

  async function dispatchDeletePatient(id: string) {
    await API.patients.deletePatient(id);
    removePatient(id);
  }

  async function dispatchGetPatient(id: string) {
    const { data } = await API.patients.getPatient(id);
    patientDetails.value = data;
    return data;
  }

  async function dispatchUpdatePatient(id: string, patient: PatientDetailsDto) {
    await API.patients.updatePatient(id, patient);
    updatePatientDetails(patient);
  }

  async function dispatchGetPatientsList() {
    const { data } = await API.patients.getPatientsList();
    return data as PatientAutocompleteDto[];
  }

  async function dispatchGetDepartmentsAutocomplete() {
    const { data } = await API.departments.getDepartmentsAutocomplete();
    departments.value = data;
    return data as DepartmentAutocompleteDto[];
  }

  async function dispatchGetRoomsList() {
    const { data } = await API.rooms.getRoomsList();
    rooms.value = data;
    return data as RoomAutocompleteDto[];
  }

  return {
    patients,
    totalItems,
    patientDetails,
    departments,
    rooms,
    dispatchGetPatients,
    dispatchCreatePatient,
    dispatchDeletePatient,
    dispatchGetPatient,
    dispatchUpdatePatient,
    dispatchGetPatientsList,
    dispatchGetDepartmentsAutocomplete,
    dispatchGetRoomsList,
    dispatchGetDoctorPatients
  };
});
