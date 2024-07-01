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
