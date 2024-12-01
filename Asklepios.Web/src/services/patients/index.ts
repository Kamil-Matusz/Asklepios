import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type PatientDto, type PatientDetailsDto, type PatientListDto, type PatientAutocompleteDto } from '@/models/Patients/patient';

const base = 'patients-module/Patients';

async function getPatient(id: string) {
  return await httpClient.get<PatientDetailsDto>(`${base}/${id}`);
}

async function getAllPatients(pagination: PaginationParams) {
  return await httpClient.get<PatientListDto[]>(base, { params: pagination });
}

async function createPatient(patient: PatientDto) {
  return await httpClient.post<void>(base, patient);
}

async function updatePatient(id: string, patient: PatientDetailsDto) {
  return await httpClient.put<void>(`${base}/${id}`, patient);
}

async function deletePatient(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

async function getPatientsList() {
  return await httpClient.get<PatientAutocompleteDto[]>(`${base}/patientsList`);
}

async function getAllPatientsByDoctor(pagination: PaginationParams, isDischarged?: boolean) {
  return await httpClient.get<PatientListDto[]>(`${base}/yourPatients`, {
    params: {
      ...pagination,
      isDischarged
    }
  });
}

export default {
  getPatient,
  getAllPatients,
  createPatient,
  updatePatient,
  deletePatient,
  getPatientsList,
  getAllPatientsByDoctor
};
