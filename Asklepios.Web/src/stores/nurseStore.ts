import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type NurseDto, type NurseListDto } from '@/models/Users/nurse';
import { type PaginationParams } from '@/models/paginationParams';
import { DepartmentAutocompleteDto } from '@/models/Departments/department';

export const useNurseStore = defineStore('nurseStore', () => {
  const nurses = ref<NurseListDto[]>([]);
  const totalItems = ref(0);
  const nurseDetails = ref<NurseDto | null>(null);
  const departments = ref<DepartmentAutocompleteDto[]>([]);

  function addNewNurse(nurse: NurseListDto) {
    nurses.value.push(nurse);
  }

  function removeNurse(id: string): boolean {
    const idx = nurses.value.findIndex((n) => n.nurseId === id);
    if (idx === -1) return false;
    nurses.value.splice(idx, 1);
    return true;
  }

  function updateNurseDetails(updatedNurse: NurseDto): boolean {
    const existingNurseIndex = nurses.value.findIndex((n) => n.nurseId === updatedNurse.nurseId);

    if (existingNurseIndex !== -1) {
      nurses.value[existingNurseIndex] = updatedNurse;
      return true;
    }
    return false;
  }

  async function dispatchGetNurses(pagination: PaginationParams) {
    const { data } = await API.nurses.getAllNurses(pagination);
    nurses.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateNurse(nurse: NurseDto) {
    await API.nurses.createNurse(nurse);
    await dispatchGetNurses({ pageIndex: 1, pageSize: 10 });
  }

  async function dispatchDeleteNurse(id: string) {
    await API.nurses.deleteNurse(id);
    removeNurse(id);
  }

  async function dispatchGetNurse(id: string) {
    const { data } = await API.nurses.getNurse(id);
    nurseDetails.value = data;
    return data;
  }

  async function dispatchUpdateNurse(id: string, nurse: NurseDto) {
    await API.nurses.updateNurse(id, nurse);
    updateNurseDetails(nurse);
  }

  async function dispatchGetDepartmentsAutocomplete() {
    const { data } = await API.departments.getDepartmentsAutocomplete();
    departments.value = data;
    return data as DepartmentAutocompleteDto[];
  }

  return {
    nurses,
    totalItems,
    nurseDetails,
    departments,
    dispatchGetNurses,
    dispatchCreateNurse,
    dispatchDeleteNurse,
    dispatchGetNurse,
    dispatchUpdateNurse,
    dispatchGetDepartmentsAutocomplete
  };
});
