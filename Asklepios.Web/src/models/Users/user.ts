export interface User {
  userId: string
  email: string
  role: string
  isActive: boolean
  createdAt: Date
}

export class InputCreateUser {
  email: string = ''
  password: string = ''
  role: string = ''
  isActive: boolean = true
}

export class InputGenerateUserAccount {
  email: string = ''
  role: string = ''
  isActive: boolean = true
}
