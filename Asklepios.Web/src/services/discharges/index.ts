import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type DischargeDto, type InputCreateDischarge, type DischargePersonDto, type DischargeItemDto } from '@/models/Patients/discharge';

const base = 'patients-module/Discharges';

async function getPaginatedDischarges(pagination: PaginationParams) {
  return await httpClient.get<DischargeItemDto[]>(base, { params: pagination });
}

async function deleteDischarge(dischargeId: string) {
  return await httpClient.delete<boolean>(`${base}/${dischargeId}`);
}

async function createDischarge(input: InputCreateDischarge) {
  return await httpClient.post<void>(base, input);
}

async function getDischarge(dischargeId: string) {
  return await httpClient.get<DischargeItemDto>(`${base}/${dischargeId}`);
}

async function updateDischarge(dischargeId: string, discharge: DischargeDto) {
  return await httpClient.put<void>(`${base}/${dischargeId}`, discharge);
}

async function dischargePatient(input: DischargePersonDto) {
  console.log('Sending dischargePatient request:', input);
  try {
    const response = await httpClient.post<void>(`${base}/dischargePatient`, input);
    console.log('dischargePatient response:', response);
    return response;
  } catch (error) {
    console.error('Error in dischargePatient:', error);
    throw error;
  }
}
async function getDoctorDischarges() {
  return await httpClient.get<DischargeItemDto[]>(`${base}/yoursDischarges`);
}

async function getAllDischarges() {
  return await httpClient.get<DischargeItemDto[]>(`${base}/allDischarges`);
}

export default {
  getPaginatedDischarges,
  deleteDischarge,
  createDischarge,
  getDischarge,
  updateDischarge,
  dischargePatient,
  getDoctorDischarges,
  getAllDischarges
};
