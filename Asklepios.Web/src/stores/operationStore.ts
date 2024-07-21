import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type OperationDto, type InputCreateOperation, type OperationItemDto } from '@/models/Examinations/operation';
import { type PaginationParams } from '@/models/paginationParams';

export const useOperationStore = defineStore('operationsStore', () => {
  const operations = ref<OperationItemDto[]>([]);
  const totalItems = ref(0);
  const operationInfo = ref<OperationDto | null>(null);

  function addNewOperation(operation: OperationItemDto) {
    operations.value.push(operation);
  }

  function removeOperation(operationId: string): boolean {
    const idx = operations.value.findIndex((s) => s.operationId === operationId);
    if (idx === -1) return false;
    operations.value.splice(idx, 1);
    return true;
  }

  function updateOperation(updatedOperation: OperationItemDto): boolean {
    const existingOperationIndex = operations.value.findIndex((m) => m.operationId === updatedOperation.operationId);

    if (existingOperationIndex !== -1) {
      operations.value[existingOperationIndex] = updatedOperation;
      return true;
    }
    return false;
  }

  async function dispatchGetOperations(pagination: PaginationParams) {
    const { data } = await API.operations.getPaginatedOperations(pagination);
    operations.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateOperation(input: InputCreateOperation) {
    const { data } = await API.operations.createOperation(input);
    // Assuming the backend returns the created operation in the response
    addNewOperation(data);
  }

  async function dispatchDeleteOperation(operationId: string) {
    await API.operations.deleteOperation(operationId);
    removeOperation(operationId);
  }

  async function dispatchGetOperation(operationId: string) {
    const { data } = await API.operations.getOperationInfo(operationId);
    operationInfo.value = data;
  }

  async function dispatchUpdateOperation(operationId: string, input: OperationDto) {
    await API.operations.updateOperation(operationId, input);
    const updatedOperation = operations.value.find((operation) => operation.operationId === operationId);
    if (updatedOperation) {
      Object.assign(updatedOperation, input);
    }
  }

  async function dispatchGetOperationsByDoctor(doctorId: string, pagination: PaginationParams) {
    const { data } = await API.operations.getOperationsByDoctor(doctorId, pagination);
    operations.value = data;
    totalItems.value = data.length;
  }

  return {
    operations,
    totalItems,
    operationInfo,
    dispatchGetOperations,
    dispatchCreateOperation,
    dispatchDeleteOperation,
    dispatchGetOperation,
    dispatchUpdateOperation,
    dispatchGetOperationsByDoctor,
    updateOperation
  };
});
