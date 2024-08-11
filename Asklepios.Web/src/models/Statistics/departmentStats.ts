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
