import httpClient from '../httpClient';
import { ClinicPatientDto } from '@/models/Clinics/clinicPatient';

const base = 'clinics-module/ClinicPatients';

async function getClinicPatient(id: string) {
  return await httpClient.get<ClinicPatientDto>(`${base}/${id}`);
}

async function getAllClinicPatients(pageIndex: number, pageSize: number) {
  return await httpClient.get<ClinicPatientDto[]>(`${base}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
}

async function createClinicPatient(patient: ClinicPatientDto) {
  return await httpClient.post<void>(base, patient);
}

async function updateClinicPatient(id: string, patient: ClinicPatientDto) {
  return await httpClient.put<void>(`${base}/${id}`, patient);
}

async function deleteClinicPatient(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

async function getClinicPatientsList() {
  return await httpClient.get<ClinicPatientDto[]>(`${base}/clinicPatientsList`);
}

export default {
  getClinicPatient,
  getAllClinicPatients,
  createClinicPatient,
  updateClinicPatient,
  deleteClinicPatient,
  getClinicPatientsList
};
