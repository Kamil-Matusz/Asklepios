export interface ClinicAppointmentDetailsDto {
  appointmentDetailsId: string;
  description: string;
  recommendations: string;
  prescriptions: string;
  doctorComments: string;
  appointmentId: string;
}

export class ClinicAppointmentDetailsRequestDto {
  appointmentDetailsId: string = '';
  description: string = '';
  recommendations: string = '';
  prescriptions: string = '';
  doctorComments: string = '';
  appointmentId: string = '';
}
