export interface ClinicAppointmentDto {
  appointmentId: string;
  appointmentDate: Date;
  appointmentType: string;
  clinicPatientId: string;
  medicalStaffId: string;
  status: string;
}

export class ClinicAppointmentRequestDto {
  patientName: string = '';
  patientSurname: string = '';
  peselNumber: string = '';
  contactNumber: string = '';
  email: string = '';
  appointmentDate: string = '';
  appointmentType: string = '';
  medicalStaffId: string = '';
  status: string = '';
}

export class ClinicAppointmentRequestByUserDto {
  patientName: string = '';
  patientSurname: string = '';
  peselNumber: string = '';
  contactNumber: string = '';
  email: string = '';
  appointmentDate: string = '';
  appointmentType: string = '';
  medicalStaffId: string = '';
  status: string = '';
  userId: string = '';
}

export class ClinicAppointmentStatusDto {
  appointmentId: string = '';
  status: string = '';
}


export interface ClinicAppointmentListDto {
  appointmentId: string;
  appointmentDate: Date;
  appointmentType: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  doctorName: string;
  doctorSurname: string;
  status: string;
}



