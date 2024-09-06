import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type OperationDto, type InputCreateOperation, type OperationItemDto } from '@/models/Examinations/operation';

const base = 'examinations-module/Operations';

async function getPaginatedOperations(pagination: PaginationParams) {
  return await httpClient.get<OperationItemDto[]>(base, { params: pagination });
}

async function deleteOperation(operationId: string) {
  return await httpClient.delete<boolean>(`${base}/${operationId}`);
}

async function createOperation(input: InputCreateOperation) {
  return await httpClient.post<OperationDto>(base, input);
}

async function getOperationInfo(operationId: string) {
  return await httpClient.get<OperationDto>(`${base}/${operationId}`);
}

async function updateOperation(operationId: string, input: OperationDto) {
  return await httpClient.put<void>(`${base}/${operationId}`, input, {
    headers: {
      'Content-Type': 'application/json',
    },
  });
}

async function getOperationsByDoctor(doctorId: string, pagination: PaginationParams) {
  return await httpClient.get<OperationItemDto[]>(`${base}/operationByDoctor/${doctorId}`, { params: pagination });
}

export default {
  getPaginatedOperations,
  deleteOperation,
  createOperation,
  getOperationInfo,
  updateOperation,
  getOperationsByDoctor
};
