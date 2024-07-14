export interface OperationDto {
  operationId: string;
  operationName: string;
  startDate: Date;
  endDate: Date;
  anesthesiaType: string
  result: string;
  complications: string
  patientId: string
  medicalStaffId: string
}

export interface Operation {
  operationId: string;
  operationName: string;
  startDate: Date;
  endDate: Date;
  anesthesiaType: string
  result: string;
  complications: string
  patientId: string
  medicalStaffId: string
}

export interface InputCreateOperation {
  operationName: string;
  startDate: Date;
  endDate: Date;
  anesthesiaType: string
  result: string;
  complications: string
  patientId: string
  medicalStaffId: string
}

export interface OperationItemDto {
  operationId: string;
  operationName: string;
  startDate: Date;
  endDate: Date;
  anesthesiaType: string
  result: string;
  complications: string
  doctorName: string
  doctornSurname: string
  patientName: string
  patientSurname: string
}
