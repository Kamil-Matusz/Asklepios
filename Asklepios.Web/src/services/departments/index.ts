import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type DepartmentDto, type DepartmentDetailsDto, type DepartmentListDto } from '@/models/Departments/department';

const base = 'departments-module/Departments';

async function getDepartment(id: string) {
  return await httpClient.get<DepartmentDetailsDto>(`${base}/${id}`);
}

async function getAllDepartments(pagination: PaginationParams) {
  return await httpClient.get<DepartmentListDto[]>(base, { params: pagination });
}

async function createDepartment(department: DepartmentDto) {
  return await httpClient.post<void>(base, department);
}

async function updateDepartment(id: string, department: DepartmentDetailsDto) {
  return await httpClient.put<void>(`${base}/${id}`, department);
}

async function deleteDepartment(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

export default {
  getDepartment,
  getAllDepartments,
  createDepartment,
  updateDepartment,
  deleteDepartment,
};
