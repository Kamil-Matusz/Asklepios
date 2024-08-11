import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type DepartmentStatsDto } from '@/models/Statistics/departmentStats';

export const useDepartmentStatsStore = defineStore('departmentStatsStore', () => {
  const departmentStats = ref<DepartmentStatsDto | null>(null);
  const allDepartmentStats = ref<DepartmentStatsDto | null>(null);
  const totalPatientsCount = ref<number>(0);
  const totalDepartmentsCount = ref<number>(0);

  async function dispatchGetDepartmentStats(departmentId: string) {
    const { data } = await API.statistic.getDepartmentStats(departmentId);
    departmentStats.value = data;
    return data;
  }

  async function dispatchGetAllDepartmentStats() {
    const { data } = await API.statistic.getAllDepartmentStats();
    allDepartmentStats.value = data;
    return data;
  }

  async function dispatchGetTotalPatientsCount() {
    const { data } = await API.statistic.getTotalPatientsCount();
    totalPatientsCount.value = data;
    return data;
  }

  async function dispatchGetTotalDepartmentsCount() {
    const { data } = await API.statistic.getTotalDepartmentsCount();
    totalDepartmentsCount.value = data;
    return data;
  }

  return {
    departmentStats,
    allDepartmentStats,
    totalPatientsCount,
    totalDepartmentsCount,
    dispatchGetDepartmentStats,
    dispatchGetAllDepartmentStats,
    dispatchGetTotalPatientsCount,
    dispatchGetTotalDepartmentsCount
  };
});
