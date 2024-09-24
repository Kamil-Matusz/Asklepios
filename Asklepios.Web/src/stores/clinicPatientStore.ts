import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { ClinicPatientDto } from '@/models/Clinics/clinicPatient';

export const useClinicPatientsStore = defineStore('clinicPatientsStore', () => {
  const patients = ref<ClinicPatientDto[]>([]);
  const patientDetails = ref<ClinicPatientDto | null>(null);

  async function dispatchGetClinicPatient(id: string) {
    const { data } = await API.clinicPatients.getClinicPatient(id);
    patientDetails.value = data;
    return data;
  }

  async function dispatchGetAllClinicPatients(pageIndex: number, pageSize: number) {
    const { data } = await API.clinicPatients.getAllClinicPatients(pageIndex, pageSize);
    patients.value = data;
  }

  async function dispatchCreateClinicPatient(patient: ClinicPatientDto) {
    await API.clinicPatients.createClinicPatient(patient);
  }

  async function dispatchUpdateClinicPatient(id: string, patient: ClinicPatientDto) {
    await API.clinicPatients.updateClinicPatient(id, patient);
  }

  async function dispatchDeleteClinicPatient(id: string) {
    await API.clinicPatients.deleteClinicPatient(id);
  }

  async function dispatchGetClinicPatientsList() {
    const { data } = await API.clinicPatients.getClinicPatientsList();
    patients.value = data;
  }

  return {
    patients,
    patientDetails,
    dispatchGetClinicPatient,
    dispatchGetAllClinicPatients,
    dispatchCreateClinicPatient,
    dispatchUpdateClinicPatient,
    dispatchDeleteClinicPatient,
    dispatchGetClinicPatientsList
  };
});
