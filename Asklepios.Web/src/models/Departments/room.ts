import {PatientDto} from "@/models/Patients/patient"

export interface Room {
  roomId: string
  roomNumber: number
  roomType: string
  numberOfBeds: number
  departmentName: string
}

export class InputCreateRoom {
  roomNumber: number = 0
  roomType: string = ''
  numberOfBeds: number = 0
  departmentId: string = ''
}

export interface RoomDto {
  roomId: string
  roomNumber: number
  roomType: string
  numberOfBeds: number
  departmentId: string
}

export interface RoomListDto {
  roomId: string
  roomNumber: number
  roomType: string
  numberOfBeds: number
  departmentName: string
}

export interface RoomDetailsDto extends RoomDto {
  patients: PatientDto[];
}

export interface RoomAutocompleteDto {
  roomId: string
  roomNumber: number
  roomType: string
}

