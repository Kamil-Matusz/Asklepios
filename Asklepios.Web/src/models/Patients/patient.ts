import {OperationDto} from "@/models/Examinations/operation"

export interface PatientDto {
  patientId: string;
  patientName: string;
  patientSurname: number;
  peselNumber: string;
  initialDiagnosis: string;
  isDischarged: boolean;
  treatment: string;
  departmentId: string;
  roomId: string;
}

export interface PatientListDto {
  patientId: string;
  patientName: string;
  patientSurname: number;
  peselNumber: string;
  initialDiagnosis: string;
  isDischarged: boolean;
  treatment: string;
  departmentName: string;
  roomNumber: string;
}

export interface PatientDetailsDto extends PatientDto {
  operations: OperationDto[];
  examResults: ExamResultDto[];
}
