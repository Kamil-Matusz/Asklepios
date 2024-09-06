import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { PatientHistoryDto } from '@/models/Patients/patientHistory';

export const usePatientHistoryStore = defineStore('patientHistoryStore', () => {
  const patientHistory = ref<PatientHistoryDto | null>(null);

  async function dispatchGetPatientHistoryByPesel(peselNumber: string) {
    const { data } = await API.patientHistories.getPatientHistoryByPesel(peselNumber);
    patientHistory.value = data;
    return data;
  }

  async function dispatchDeletePatientHistory(historyId: string) {
    await API.patientHistories.deletePatientHistory(historyId);
    patientHistory.value = null;
  }

  return {
    patientHistory,
    dispatchGetPatientHistoryByPesel,
    dispatchDeletePatientHistory
  };
});
