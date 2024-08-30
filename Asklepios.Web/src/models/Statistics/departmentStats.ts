export interface DepartmentDetailDto {
  departmentName: string;
  totalBeds: number;
  patientsCount: number;
  totalRooms: number;
}

export interface DepartmentStatsDto {
  totalDepartments: number;
  departmentDetails: Record<string, DepartmentDetailDto>;
}

export interface MonthlyDischargeSummary {
  dischargeMonth: string;
  patientCount: number;
}

export interface MonthlyAdmissionSummary {
  admissionMonth: string;
  patientCount: number;
}
