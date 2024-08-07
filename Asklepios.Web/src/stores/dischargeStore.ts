import { defineStore } from 'pinia';
import { ref } from 'vue';
import {API} from '@/services'
import { type DischargeDto, type InputCreateDischarge, type DischargePersonDto, type DischargeItemDto } from '@/models/Patients/discharge';
import { type PaginationParams } from '@/models/paginationParams';

export const useDischargeStore = defineStore('dischargeStore', () => {
  const discharges = ref<DischargeItemDto[]>([]);
  const totalItems = ref(0);
  const dischargeDetails = ref<DischargeDto | null>(null);

  function addNewDischarge(discharge: DischargeItemDto) {
    discharges.value.push(discharge);
  }

  function removeDischarge(dischargeId: string): boolean {
    const idx = discharges.value.findIndex((d) => d.dischargeId === dischargeId);
    if (idx === -1) return false;
    discharges.value.splice(idx, 1);
    return true;
  }

  function updateDischarge(updatedDischarge: DischargeDto): boolean {
    const existingDischargeIndex = discharges.value.findIndex((d) => d.dischargeId === updatedDischarge.dischargeId);

    if (existingDischargeIndex !== -1) {
      discharges.value[existingDischargeIndex] = updatedDischarge;
      return true;
    }
    return false;
  }

  async function dispatchGetDischarges(pagination: PaginationParams) {
    const { data } = await API.discharges.getPaginatedDischarges(pagination);
    discharges.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateDischarge(input: InputCreateDischarge) {
    await API.discharges.createDischarge(input);
    await dispatchGetDischarges({ pageIndex: 1, pageSize: 10 });
  }

  async function dispatchDeleteDischarge(dischargeId: string) {
    await API.discharges.deleteDischarge(dischargeId);
    removeDischarge(dischargeId);
  }

  async function dispatchGetDischarge(dischargeId: string) {
    const { data } = await API.discharges.getDischarge(dischargeId);
    dischargeDetails.value = data;
    return data;
  }

  async function dispatchUpdateDischarge(dischargeId: string, discharge: DischargeDto) {
    await API.discharges.updateDischarge(dischargeId, discharge);
    updateDischarge(discharge);
  }

  async function dispatchDischargePatient(input: DischargePersonDto) {
    await API.discharges.dischargePatient(input);
  }

  return {
    discharges,
    totalItems,
    dischargeDetails,
    dispatchGetDischarges,
    dispatchCreateDischarge,
    dispatchDeleteDischarge,
    dispatchGetDischarge,
    dispatchUpdateDischarge,
    dispatchDischargePatient,
  };
});
