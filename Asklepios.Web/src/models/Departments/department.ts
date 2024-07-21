import {RoomDto} from "@/models/Departments/room"
import {PatientDto} from "@/models/Patients/patient"

export interface Department {
  departmentId: string
  departmentName: string
  numberOfBeds: number
  actualNumberOfPatients: number
}

export class InputCreateDepartment {
  departmentName: string = ''
  numberOfBeds: number = 0
  actualNumberOfPatients: number = 0
}

export interface DepartmentDetailsDto extends DepartmentDto {
  rooms: RoomDto[];
  patients: PatientDto[];
}

export interface DepartmentDto {
  departmentId: string;
  departmentName: string;
  numberOfBeds: number;
  actualNumberOfPatients: number;
}


export interface DepartmentListDto {
  departmentId: string;
  departmentName: string;
  numberOfBeds: number;
  actualNumberOfPatients: number;
}
