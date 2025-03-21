import {OperationDto} from "@/models/Examinations/operation"
import { ExamResultDto } from "../Examinations/examResult";

export interface PatientDto {
  patientId: string;
  patientName: string;
  patientSurname: string;
  peselNumber: string;
  initialDiagnosis: string;
  isDischarged: boolean;
  treatment: string;
  departmentId: string;
  roomId: string;
  admissionDate: Date;
  address: string;
}

export interface PatientListDto {
  patientId: string;
  patientName: string;
  patientSurname: string;
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

export interface PatientAutocompleteDto {
  patientId: string;
  patientName: string;
  patientSurname: number;
  peselNumber: string;
}

export class InputCreatePatient {
  patientName: string = '';
  patientSurname: string = '';
  peselNumber: string = '';
  initialDiagnosis: string = '';
  isDischarged: boolean = false;
  treatment: string = '';
  departmentId: string = '';
  roomId: string = '';
  medicalStaffId: string = '';
  address: string = '';
}


