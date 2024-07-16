export class InputCreateMedicalStaff {
  name: string = '';
  surname: string = '';
  privatePhoneNumber: string = '';
  hospitalPhoneNumber: string = '';
  specialization: string = '';
  medicalLicenseNumber: string = '';
  userId: string = '';
  departmentId: string = '';
}

export interface MedicalStaffDto {
  doctorId: string;
  name: string;
  surname: string;
  privatePhoneNumber: string;
  hospitalPhoneNumber: string;
  specialization: string;
  medicalLicenseNumber: string;
  userId: string;
  departmentId: string;
}

export interface MedicalStaffListDto {
  doctorId: string;
  name: string;
  surname: string;
  privatePhoneNumber: string;
  hospitalPhoneNumber: string;
  specialization: string;
  medicalLicenseNumber: string;
  departmentName: string;
}
