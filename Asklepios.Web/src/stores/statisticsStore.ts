import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { MonthlyDischargeSummary, type DepartmentStatsDto } from '@/models/Statistics/departmentStats';

export const useDepartmentStatsStore = defineStore('departmentStatsStore', () => {
  const departmentStats = ref<DepartmentStatsDto | null>(null);
  const allDepartmentStats = ref<DepartmentStatsDto | null>(null);
  const monthlyDischargeSummary = ref<MonthlyDischargeSummary | null>(null);
  const totalPatientsCount = ref<number>(0);
  const totalDepartmentsCount = ref<number>(0);
  const totalDoctorsCount = ref<number>(0);
  const totalNursesCount = ref<number>(0);

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

  async function dispatchGetTotalDoctorsCount() {
    const { data } = await API.statistic.getTotalDoctorsCount();
    totalDoctorsCount.value = data;
    return data;
  }

  async function dispatchGetTotalNursesCount() {
    const { data } = await API.statistic.getTotalNursesCount();
    totalNursesCount.value = data;
    return data;
  }

  async function dispatchMonthlyDischarges() {
    const { data } = await API.statistic.getMonthlyDischarges();
    monthlyDischargeSummary.value = data;
    return data;
  }

  return {
    departmentStats,
    allDepartmentStats,
    totalPatientsCount,
    totalDepartmentsCount,
    totalDoctorsCount,
    totalNursesCount,
    monthlyDischargeSummary,
    dispatchGetDepartmentStats,
    dispatchGetAllDepartmentStats,
    dispatchGetTotalPatientsCount,
    dispatchGetTotalDepartmentsCount,
    dispatchGetTotalDoctorsCount,
    dispatchGetTotalNursesCount,
    dispatchMonthlyDischarges
  };
});
