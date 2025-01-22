import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { ClinicAppointmentDetailsDto } from '@/models/Clinics/clinicAppointmentDetails';

export const useClinicAppointmentDetailsStore = defineStore('clinicAppointmentDetailsStore', () => {
  const admissionDetails = ref<ClinicAppointmentDetailsDto | null>(null);
  const allAdmissionDetails = ref<ClinicAppointmentDetailsDto[]>([]);

  async function dispatchCreateAdmissionDetails(details: ClinicAppointmentDetailsDto) {
    await API.clinicAppointmentDetails.createAppointmentDetails(details);
  }

  async function dispatchDeleteAdmissionDetails(appointmentId: string) {
    await API.clinicAppointmentDetails.deleteAppointmentDetails(appointmentId);
  }

  async function dispatchUpdateAdmissionDetails(appointmentId: string, details: ClinicAppointmentDetailsDto) {
    await API.clinicAppointmentDetails.updateAppointmentDetails(appointmentId, details);
  }

  async function dispatchGetAdmissionDetailsById(appointmentId: string) {
    try {
      const { data } = await API.clinicAppointmentDetails.getAppointmentDetailsById(appointmentId);
      admissionDetails.value = data;
      return data;
    } catch (error) {
      console.error("Błąd podczas pobierania szczegółów wizyty:", error);
      admissionDetails.value = null;
    }
  }

  return {
    admissionDetails,
    allAdmissionDetails,
    dispatchCreateAdmissionDetails,
    dispatchDeleteAdmissionDetails,
    dispatchUpdateAdmissionDetails,
    dispatchGetAdmissionDetailsById,
  };
});
