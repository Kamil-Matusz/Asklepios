import httpClient from '../httpClient';
import { ClinicAppointmentDto, ClinicAppointmentRequestDto, ClinicAppointmentStatusDto, ClinicAppointmentListDto } from '@/models/Clinics/clinicAppointment';

const base = 'clinics-module/ClinicAppointments';

async function createAppointment(appointment: ClinicAppointmentRequestDto) {
  return await httpClient.post<void>(base, appointment);
}

async function deleteAppointment(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

async function updateAppointmentStatus(id: string, statusDto: ClinicAppointmentStatusDto) {
  return await httpClient.put<void>(`${base}/${id}`, statusDto);
}

async function getAppointment(id: string) {
  return await httpClient.get<ClinicAppointmentListDto>(`${base}/${id}`);
}

export default {
  createAppointment,
  deleteAppointment,
  updateAppointmentStatus,
  getAppointment
};
