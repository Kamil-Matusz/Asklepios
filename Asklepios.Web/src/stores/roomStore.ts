// roomsStore.ts
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { RoomDto, RoomDetailsDto, RoomListDto } from '@/models/Departments/room';
import { PaginationParams } from '@/models/paginationParams';

export const useRoomStore = defineStore('roomsStore', () => {
  const rooms = ref<RoomListDto[]>([]);
  const totalItems = ref(0);
  const roomDetails = ref<RoomDetailsDto | null>(null);

  function addNewRoom(room: RoomDto) {
    rooms.value.push(room);
  }

  function removeRoom(id: string): boolean {
    const idx = rooms.value.findIndex((r) => r.roomId === id);
    if (idx === -1) return false;
    rooms.value.splice(idx, 1);
    return true;
  }

  function updateRoomDetails(updatedRoom: RoomDetailsDto): boolean {
    const existingRoomIndex = rooms.value.findIndex((r) => r.roomId === updatedRoom.roomId);

    if (existingRoomIndex !== -1) {
      rooms.value[existingRoomIndex] = updatedRoom;
      return true;
    }
    return false;
  }

  async function dispatchGetRooms(pagination: PaginationParams) {
    const { data } = await API.rooms.getAllRooms(pagination);
    rooms.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateRoom(room: RoomDto) {
    await API.rooms.createRoom(room);
    addNewRoom(room);
  }

  async function dispatchDeleteRoom(id: string) {
    await API.rooms.deleteRoom(id);
    removeRoom(id);
  }

  async function dispatchGetRoom(id: string) {
    const { data } = await API.rooms.getRoom(id);
    roomDetails.value = data;
    return data;
  }

  async function dispatchUpdateRoom(id: string, room: RoomDetailsDto) {
    await API.rooms.updateRoom(id, room);
    updateRoomDetails(room);
  }

  return {
    rooms,
    totalItems,
    roomDetails,
    dispatchGetRooms,
    dispatchCreateRoom,
    dispatchDeleteRoom,
    dispatchGetRoom,
    dispatchUpdateRoom,
  };
});