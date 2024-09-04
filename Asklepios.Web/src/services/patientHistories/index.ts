import httpClient from '../httpClient';
import { PatientHistoryDto } from '@/models/Patients/patientHistory';

const base = 'patients-module/PatientHistories';

async function getPatientHistoryByPesel(peselNumber: string) {
  return await httpClient.get<PatientHistoryDto>(`${base}/${peselNumber}`);
}

async function deletePatientHistory(historyId: string) {
  return await httpClient.delete<void>(`${base}/${historyId}`);
}

export default {
  getPatientHistoryByPesel,
  deletePatientHistory
};
