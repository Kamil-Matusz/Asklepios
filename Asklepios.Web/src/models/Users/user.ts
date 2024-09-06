export interface User {
  userId: string;
  email: string;
  role: string;
  isActive: boolean;
  createdAt: Date;
}

export interface UserAutocompleteDto {
  userId: string;
  email: string;
}

export interface InputCreateUser {
  email: string;
  password: string;
  role: string;
}

export interface JwtDto {
  accessToken: string;
}

export interface AccountDto {
  userId: string;
  email: string;
  role: string;
  isActive: boolean;
  createdAt: Date;
}

export interface GenerateUserAccount {
  role: string;
  email: string;
  isActive: boolean;
}

export interface ChangeUserPassword {
  password: string;
}
