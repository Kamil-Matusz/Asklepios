import httpClient from '../httpClient';
import { ClinicAppointmentDetailsDto } from '@/models/Clinics/clinicAppointmentDetails';

const base = 'clinics-module/ClinicAppointmentDetails';

async function createAppointmentDetails(details: ClinicAppointmentDetailsDto) {
  return await httpClient.post<void>(base, details);
}

async function deleteAppointmentDetails(appointmentId: string) {
  return await httpClient.delete<void>(`${base}/${appointmentId}`);
}

async function updateAppointmentDetails(appointmentId: string, details: ClinicAppointmentDetailsDto) {
  return await httpClient.put<void>(`${base}/${appointmentId}`, details);
}

async function getAppointmentDetailsById(appointmentId: string) {
  return await httpClient.get<ClinicAppointmentDetailsDto>(`${base}/${appointmentId}`);
}

export default {
  createAppointmentDetails,
  deleteAppointmentDetails,
  updateAppointmentDetails,
  getAppointmentDetailsById,
};
