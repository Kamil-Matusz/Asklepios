export interface DischargeDto {
  dischargeId: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  address: string;
  date: Date;
  dischargeReasson: string;
  summary: string;
  medicalStaffId: string;
}

export interface InputCreateDischarge {
  dischargeId: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  address: string;
  date: Date;
  dischargeReasson: string;
  summary: string;
  medicalStaffId: string;
}
export interface DischargePersonDto {
  patientId: string;
  dischargeDate: Date;
  dischargeReasson: string;
  summary: string;
  medicalStaffId: string;
}

export interface DischargeItemDto {
  dischargeId: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  address: string;
  date: Date;
  dischargeReasson: string;
  summary: string;
  doctorName: string;
  doctorSurname: string;
}
