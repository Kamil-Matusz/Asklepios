import httpClient from '../httpClient';
import { type DepartmentStatsDto } from '@/models/Statistics/departmentStats';

const base = 'statistics-module/DepartmentStatistics';

async function getDepartmentStats(departmentId: string) {
  return await httpClient.get<DepartmentStatsDto>(`${base}/${departmentId}`);
}

async function getAllDepartmentStats() {
  return await httpClient.get<DepartmentStatsDto>(`${base}/allStats`);
}

async function getTotalPatientsCount() {
  return await httpClient.get<number>(`${base}/totalPatients`);
}

export default {
  getDepartmentStats,
  getAllDepartmentStats,
  getTotalPatientsCount
};
