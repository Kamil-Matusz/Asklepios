export interface NurseDto {
  nurseId: string
  name: string,
  surname: string,
  departmenId: string,
  userId: string
}

export class InputCreateNurse {
  name: string = ''
  surname: string = ''
  userId: string = ''
  departmentId: string = ''
}

export interface NurseListDto {
  nurseId: string
  name: string,
  surname: string,
  departmentName: string,
}
