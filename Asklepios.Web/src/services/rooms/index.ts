// roomsService.ts
import { PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { RoomDto, RoomDetailsDto, RoomListDto } from '@/models/Departments/room';

const base = 'rooms-module/Rooms';

async function getRoom(id: string) {
  return await httpClient.get<RoomDetailsDto>(`${base}/${id}`);
}

async function getAllRooms(pagination: PaginationParams) {
  return await httpClient.get<RoomListDto[]>(base, { params: pagination });
}

async function createRoom(room: RoomDto) {
  return await httpClient.post<void>(base, room);
}

async function updateRoom(id: string, room: RoomDetailsDto) {
  return await httpClient.put<void>(`${base}/${id}`, room);
}

async function deleteRoom(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

export default {
  getRoom,
  getAllRooms,
  createRoom,
  updateRoom,
  deleteRoom,
};
