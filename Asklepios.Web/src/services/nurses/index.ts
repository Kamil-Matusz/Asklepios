import httpClient from '../httpClient';
import { type PaginationParams } from '@/models/paginationParams';
import { type NurseDto, type NurseListDto } from '@/models/Users/nurse';

const base = 'users-module/Nurse';

async function getNurse(id: string) {
  return await httpClient.get<NurseDto>(`${base}/${id}`);
}

async function getAllNurses(pagination: PaginationParams) {
  return await httpClient.get<NurseListDto[]>(base, { params: pagination });
}

async function createNurse(nurse: NurseDto) {
  return await httpClient.post<void>(base, nurse);
}

async function updateNurse(id: string, nurse: NurseDto) {
  return await httpClient.put<void>(`${base}/${id}`, nurse);
}

async function deleteNurse(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

export default {
  getNurse,
  getAllNurses,
  createNurse,
  updateNurse,
  deleteNurse
};
