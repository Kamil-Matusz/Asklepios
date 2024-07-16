import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type MedicalStaffDto, type MedicalStaffListDto, InputCreateMedicalStaff } from '@/models/Users/doctor';
import { type PaginationParams } from '@/models/paginationParams';

export const useMedicalStaffStore = defineStore('medicalStaffStore', () => {
  const doctors = ref<MedicalStaffListDto[]>([]);
  const totalItems = ref(0);
  const doctorDetails = ref<MedicalStaffDto | null>(null);

  function addNewDoctor(doctor: MedicalStaffListDto) {
    doctors.value.push(doctor);
  }

  function removeDoctor(id: string): boolean {
    const idx = doctors.value.findIndex((d) => d.doctorId === id);
    if (idx === -1) return false;
    doctors.value.splice(idx, 1);
    return true;
  }

  function updateDoctorDetails(updatedDoctor: MedicalStaffDto): boolean {
    const existingDoctorIndex = doctors.value.findIndex((d) => d.doctorId === updatedDoctor.doctorId);

    if (existingDoctorIndex !== -1) {
      doctors.value[existingDoctorIndex] = updatedDoctor;
      return true;
    }
    return false;
  }

  async function dispatchGetDoctors(pagination: PaginationParams) {
    const { data } = await API.doctors.getAllDoctors(pagination);
    doctors.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateDoctor(doctor: InputCreateMedicalStaff) {
    await API.doctors.createDoctor(doctor);
    await dispatchGetDoctors({ pageIndex: 0, pageSize: 10 });
  }

  async function dispatchDeleteDoctor(id: string) {
    await API.doctors.deleteDoctor(id);
    removeDoctor(id);
  }

  async function dispatchGetDoctor(id: string) {
    const { data } = await API.doctors.getDoctor(id);
    doctorDetails.value = data;
    return data;
  }

  async function dispatchUpdateDoctor(id: string, doctor: MedicalStaffDto) {
    await API.doctors.updateDoctor(id, doctor);
    updateDoctorDetails(doctor);
  }

  return {
    doctors,
    totalItems,
    doctorDetails,
    dispatchGetDoctors,
    dispatchCreateDoctor,
    dispatchDeleteDoctor,
    dispatchGetDoctor,
    dispatchUpdateDoctor
  };
});
