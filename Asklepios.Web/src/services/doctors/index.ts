import httpClient from '../httpClient';
import { type PaginationParams } from '@/models/paginationParams';
import { type MedicalStaffDto, type MedicalStaffListDto, InputCreateMedicalStaff } from '@/models/Users/doctor';

const base = 'users-module/MedicalStaff';

async function getDoctor(id: string) {
  return await httpClient.get<MedicalStaffDto>(`${base}/${id}`);
}

async function getAllDoctors(pagination: PaginationParams) {
  return await httpClient.get<MedicalStaffListDto[]>(base, { params: pagination });
}

async function createDoctor(doctor: InputCreateMedicalStaff) {
  return await httpClient.post<void>(base, doctor);
}

async function updateDoctor(id: string, doctor: MedicalStaffDto) {
  return await httpClient.put<void>(`${base}/${id}`, doctor);
}

async function deleteDoctor(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

export default {
  getDoctor,
  getAllDoctors,
  createDoctor,
  updateDoctor,
  deleteDoctor
};
