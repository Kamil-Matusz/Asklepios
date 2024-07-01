export interface Nurse {
  nurseId: string
  name: string,
  surname: string,
  departmentName: string
}

export class InputCreateNurse {
  name: string = ''
  surname: string = ''
  userId: string = ''
  departmentId: string = ''
}
