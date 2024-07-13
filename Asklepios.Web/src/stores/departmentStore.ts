import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type DepartmentDto, type DepartmentDetailsDto, type DepartmentListDto } from '@/models/Departments/department';
import { type PaginationParams } from '@/models/paginationParams';

export const useDepartmentStore = defineStore('departmentsStore', () => {
  const departments = ref<DepartmentListDto[]>([]);
  const totalItems = ref(0);
  const departmentDetails = ref<DepartmentDetailsDto | null>(null);

  function addNewDepartment(department: DepartmentDto) {
    departments.value.push(department);
  }

  function removeDepartment(id: string): boolean {
    const idx = departments.value.findIndex((d) => d.departmentId === id);
    if (idx === -1) return false;
    departments.value.splice(idx, 1);
    return true;
  }

  function updateDepartmentDetails(updatedDepartment: DepartmentDetailsDto): boolean {
    const existingDepartmentIndex = departments.value.findIndex((d) => d.departmentId === updatedDepartment.departmentId);

    if (existingDepartmentIndex !== -1) {
      departments.value[existingDepartmentIndex] = updatedDepartment;
      return true;
    }
    return false;
  }

  async function dispatchGetDepartments(pagination: PaginationParams) {
    const { data } = await API.departments.getAllDepartments(pagination);
    departments.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateDepartment(department: DepartmentDto) {
    await API.departments.createDepartment(department);
    addNewDepartment(department);
  }

  async function dispatchDeleteDepartment(id: string) {
    await API.departments.deleteDepartment(id);
    removeDepartment(id);
  }

  async function dispatchGetDepartment(id: string) {
    const { data } = await API.departments.getDepartment(id);
    departmentDetails.value = data;
    return data;
  }

  async function dispatchUpdateDepartment(id: string, department: DepartmentDetailsDto) {
    await API.departments.updateDepartment(id, department);
    updateDepartmentDetails(department);
  }

  return {
    departments,
    totalItems,
    departmentDetails,
    dispatchGetDepartments,
    dispatchCreateDepartment,
    dispatchDeleteDepartment,
    dispatchGetDepartment,
    dispatchUpdateDepartment,
  };
});
