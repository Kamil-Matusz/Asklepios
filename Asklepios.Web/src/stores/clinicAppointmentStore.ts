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
  }

  async function dispatchUpdateAppointmentStatus(id: string, statusDto: ClinicAppointmentStatusDto) {
    await API.clinicAppointments.updateAppointmentStatus(id, statusDto);
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

  async function dispatchGetAppointmentsByDate(date: string) {
    try {
      const { data } = await API.clinicAppointments.getAppointmentsByDate(date);
      appointments.value = data || [];
      return data;
    } catch (error) {
      console.error("Błąd podczas pobierania wizyt:", error);
      appointments.value = [];
    }
  }

  async function dispatchGetUserPastAppointments() {
    try {
      const { data } = await API.clinicAppointments.getUserPastAppointments();
      appointments.value = data || [];
      return data;
    } catch (error) {
      console.error("Błąd podczas pobierania wizyt:", error);
      appointments.value = [];
    }
  }

  async function dispatchGetUserFutureAppointments() {
    try {
      const { data } = await API.clinicAppointments.getUserPastAppointments();
      appointments.value = data || [];
      return data;
    } catch (error) {
      console.error("Błąd podczas pobierania wizyt:", error);
      appointments.value = [];
    }
  }


  return {
    appointments,
    appointmentDetails,
    dispatchCreateAppointment,
    dispatchDeleteAppointment,
    dispatchUpdateAppointmentStatus,
    dispatchGetAppointment,
    dispatchGetAppointments,
    dispatchGetAppointmentsByDate,
    dispatchGetUserPastAppointments,
    dispatchGetUserFutureAppointments
  };
});
