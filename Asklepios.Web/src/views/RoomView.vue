<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoomStore } from '@/stores/roomStore';
import { useToast } from 'vue-toastification';
import { type RoomDto, type RoomDetailsDto, InputCreateRoom } from '@/models/Departments/room';
import { InputPagination } from '@/models/paginationParams';
import BasePage from '@/components/pages/BasePage.vue';

const roomStore = useRoomStore();
const toast = useToast();

const pagination = ref<InputPagination>({
  PageIndex: 1,
  PageSize: 10
});

const headers = [
  { title: 'Typ pokoju', key: 'roomType', align: 'start' },
  { title: 'Numer pokoju', key: 'roomNumber', align: 'start' },
  { title: 'Liczba łóżek', key: 'numberOfBeds', align: 'start' },
  { title: 'Oddział', key: 'departmentName', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: pagination.value.PageSize,
  loading: true,
  totalItems: 0
});

const roomToAdd = ref<InputCreateRoom>({
  roomNumber: 0,
  roomType: '',
  numberOfBeds: 0,
  departmentId: ''
});

const roomToEdit = ref<RoomDetailsDto | null>(null);
const isEditDialogActive = ref(false);

const getRooms = async () => {
  options.value.loading = true;
  try {
    await roomStore.dispatchGetRooms({
      PageIndex: pagination.value.PageIndex,
      PageSize: pagination.value.PageSize
    });
    options.value.totalItems = roomStore.totalItems;
  } catch (error) {
    console.error('Error fetching rooms:', error);
    toast.error('Wystąpił problem podczas pobierania pokoi');
  } finally {
    options.value.loading = false;
  }
};

const addRoom = async (room: RoomDto) => {
  try {
    if (!room.roomNumber || room.numberOfBeds === 0) {
      console.error('Wymagane są wszystkie pola: Nazwa, Numer pokoju i Liczba łóżek');
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    console.log('Dodawanie pokoju z danymi:', room);
    await roomStore.dispatchCreateRoom(room);
    toast.success('Pomyślnie dodano nowy pokój!');
    roomToAdd.value = {roomType: '', roomNumber: '', numberOfBeds: 0 };
    getRooms();
  } catch (error) {
    console.error('Error adding room:', error);
    toast.error('Wystąpił problem podczas dodawania pokoju');
  }
};

const deleteRoom = async (id: string) => {
  await roomStore.dispatchDeleteRoom(id);
  toast.success('Pomyślnie usunięto pokój!');
  getRooms();
};

const updateRoom = async () => {
  if (roomToEdit.value) {
    await roomStore.dispatchUpdateRoom(roomToEdit.value.roomId, roomToEdit.value);
    toast.success('Pomyślnie zaktualizowano dane pokoju!');
    getRooms();
  }
};

const openEditDialog = (room: RoomDetailsDto) => {
  roomToEdit.value = { ...room };
  isEditDialogActive.value = true;
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  pagination.value.PageIndex = page;
  pagination.value.PageSize = itemsPerPage;
  getRooms();
};

onMounted(getRooms);
</script>

<template>
  <BasePage title="Zarządzanie pokojami">
    <template #above-card>
      <v-dialog max-width="500">
        <template #activator="{ props: activatorProps }">
          <v-btn
            v-bind="activatorProps"
            color="primary"
            variant="flat"
            class="mb-4"
            style="max-width: 20rem"
          >+Dodaj nowy pokój</v-btn>
        </template>

        <template #default="{ isActive }">
          <v-card title="Nowy pokój" rounded="lg">
            <RoomForm
              v-model="roomToAdd"
              @on-valid-submit="(room) => { addRoom(room); isActive.value = false; }"
            ></RoomForm>
          </v-card>
        </template>
      </v-dialog>
    </template>

    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="roomStore.rooms"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="roomId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="openEditDialog(item)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-pen"
        ></v-btn>

        <v-dialog max-width="500">
          <template #activator="{ props: activatorProps }">
            <v-btn
              rounded="lg"
              size="small"
              color="red"
              v-bind="activatorProps"
              class="ml-2"
              icon="mdi-delete"
            ></v-btn>
          </template>

          <template #default="{ isActive }">
            <v-card title="Potwierdź swoją decyzję">
              <v-card-text>
                Czy na pewno chcesz usunąć ten pokój?
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  text="Usuń pokój"
                  color="red"
                  rounded="lg"
                  @click="() => { isActive.value = false; deleteRoom(item.roomId); }"
                ></v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </v-dialog>
      </template>

      <template #item.numberOfBeds="{ item }">
        {{ item.numberOfBeds }}
      </template>
    </v-data-table-server>

    <v-dialog max-width="500" v-model="isEditDialogActive">
      <template #default>
        <v-card title="Edytuj pokój" rounded="lg">
          <RoomForm
            v-model="roomToEdit"
            @on-valid-submit="(room) => { updateRoom(room); isEditDialogActive.value = false; }"
          ></RoomForm>
        </v-card>
      </template>
    </v-dialog>
  </BasePage>
</template>
