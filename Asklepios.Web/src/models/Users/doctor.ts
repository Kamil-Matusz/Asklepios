export interface Doctor {
  doctorId: string
  name: string,
  surname: string,
  privatePhoneNumber : string,
  hospitalPhoneNumber: string,
  specialization: string,
  medicalLicenseNumber: string,
  departmentName: string
}

export class InputCreateDoctor {
  name: string = ''
  surname: string = ''
  privatePhoneNumber : string = ''
  hospitalPhoneNumber: string = ''
  specialization: string = ''
  medicalLicenseNumber: string = ''
  userId: string = ''
  departmentId: string = ''
}
