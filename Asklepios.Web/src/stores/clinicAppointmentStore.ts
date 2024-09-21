import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { ClinicAppointmentDto, ClinicAppointmentRequestDto, ClinicAppointmentStatusDto, ClinicAppointmentListDto } from '@/models/Clinics/clinicAppointment';

export const useClinicAppointmentsStore = defineStore('clinicAppointmentsStore', () => {
  const appointments = ref<ClinicAppointmentListDto[]>([]);
  const appointmentDetails = ref<ClinicAppointmentListDto | null>(null);

  async function dispatchCreateAppointment(appointment: ClinicAppointmentRequestDto) {
    await API.clinicAppointments.createAppointment(appointment);
  }

  async function dispatchDeleteAppointment(id: string) {
    await API.clinicAppointments.deleteAppointment(id);
    await dispatchGetAppointments();
  }

  async function dispatchUpdateAppointmentStatus(id: string, statusDto: ClinicAppointmentStatusDto) {
    await API.clinicAppointments.updateAppointmentStatus(id, statusDto);
    await dispatchGetAppointments();
  }

  async function dispatchGetAppointment(id: string) {
    const { data } = await API.clinicAppointments.getAppointment(id);
    appointmentDetails.value = data;
    return data;
  }

  async function dispatchGetAppointments() {
    const { data } = await API.clinicAppointments.getAllAppointments();
    appointments.value = data;
  }

  return {
    appointments,
    appointmentDetails,
    dispatchCreateAppointment,
    dispatchDeleteAppointment,
    dispatchUpdateAppointmentStatus,
    dispatchGetAppointment,
    dispatchGetAppointments
  };
});
