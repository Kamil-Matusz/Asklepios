export interface PatientVisitDto {
  admissionDate: Date;
  dischargeDate: Date;
  operationName: string;
  result: string;
  complications: string;
  anesthesiaType: string;
}

export interface PatientHistoryDto {
  historyId: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  history: PatientVisitDto[];
}
