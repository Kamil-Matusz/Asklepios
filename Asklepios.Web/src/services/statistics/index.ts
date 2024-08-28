import httpClient from '../httpClient';
import { MonthlyDischargeSummary, type DepartmentStatsDto } from '@/models/Statistics/departmentStats';

const base = 'statistics-module/Statistics';

async function getDepartmentStats(departmentId: string) {
  return await httpClient.get<DepartmentStatsDto>(`${base}/${departmentId}`);
}

async function getAllDepartmentStats() {
  return await httpClient.get<DepartmentStatsDto>(`${base}/allStats`);
}

async function getTotalPatientsCount() {
  return await httpClient.get<number>(`${base}/totalPatients`);
}

async function getTotalDepartmentsCount() {
  return await httpClient.get<number>(`${base}/totalDepartments`);
}

async function getTotalDoctorsCount() {
  return await httpClient.get<number>(`${base}/totalDoctors`);
}

async function getTotalNursesCount() {
  return await httpClient.get<number>(`${base}/totalNurses`);
}

async function getMonthlyDischarges() {
  return await httpClient.get<MonthlyDischargeSummary>(`${base}/monthlyDischarges`);
}

export default {
  getDepartmentStats,
  getAllDepartmentStats,
  getTotalPatientsCount,
  getTotalDepartmentsCount,
  getTotalDoctorsCount,
  getTotalNursesCount,
  getMonthlyDischarges,
};
