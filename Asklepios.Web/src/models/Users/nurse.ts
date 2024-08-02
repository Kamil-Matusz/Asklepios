export interface NurseDto {
  nurseId: string
  name: string,
  surname: string,
  departmentId: string,
  userId: string
}

export class InputCreateNurse {
  name: string = ''
  surname: string = ''
  departmentId: string = ''
  userId: string = ''
}

export interface NurseListDto {
  nurseId: string
  name: string,
  surname: string,
  departmentName: string,
}
