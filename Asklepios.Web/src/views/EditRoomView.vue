<template>
  <BasePage title="Edycja pokoju">
    <div v-if="form.roomNumber !== ''" class="room-details">
      <h2>Edycja pokoju {{ form.roomNumber }}</h2>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field
          label="Numer pokoju"
          v-model="form.roomNumber"
          type="number"
          required
        ></v-text-field>
        <v-select
          v-model="form.roomType"
          :items="roomTypes"
          item-title="text"
          item-value="value"
          label="Typ pokoju"
          required
        ></v-select>
        <v-text-field
          label="Liczba łóżek"
          v-model="form.numberOfBeds"
          type="number"
          required
        ></v-text-field>
        <v-select
          v-model="form.departmentId"
          label="Oddział"
          :items="formattedDepartments"
          item-title="text"
          item-value="value"
          required
        ></v-select>
        <v-btn type="submit" color="primary">Zatwierdź</v-btn>
      </v-form>
      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useRoomStore } from '@/stores/roomStore';
import { useToast } from 'vue-toastification';
import { RoomDto } from '@/models/Departments/room';

const route = useRoute();
const router = useRouter();
const roomStore = useRoomStore();
const toast = useToast();

const form = ref({
  roomNumber: '',
  roomType: '',
  numberOfBeds: 0,
  departmentId: ''
});

const roomTypes = ref([
  { text: 'Sala Ogólna', value: 'Sala Ogólna' },
  { text: 'Sala Operacyjna', value: 'Sala Operacyjna' },
  { text: 'Izolatka', value: 'Izolatka' },
]);

const fetchRoomDetails = async () => {
  try {
    const roomId = route.params.id as string;
    await roomStore.dispatchGetRoom(roomId);
    const room = roomStore.roomDetails;
    if (room) {
      form.value = { ...room };
    }
  } catch (error) {
    console.error('Error fetching room details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów pokoju');
  }
};

const formattedDepartments = computed(() => {
  return roomStore.departments?.map(department => ({
    text: department.departmentName,
    value: department.departmentId
  })) || [];
});

const handleSubmit = async () => {
  try {
    if (!form.value.roomNumber || form.value.numberOfBeds === 0) {
      throw new Error('Wszystkie pola muszą być wypełnione');
    }
    await roomStore.dispatchUpdateRoom(form.value.roomId || '', form.value);
    toast.success('Pomyślnie zaktualizowano dane pokoju!');
    router.go(-1);
  } catch (error) {
    console.error('Error updating room:', error);
    toast.error('Wystąpił problem podczas aktualizacji pokoju');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(async () => {
  await roomStore.dispatchGetDepartmentsAutocomplete();
  await fetchRoomDetails();
});
</script>

<style scoped>
.room-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.room-details h2 {
  margin-bottom: 10px;
}

.btn-custom {
  margin-top: 15px;
  padding: 10px 20px;
  border: 2px solid #4CAF50;
  border-radius: 5px;
  background-color: #4CAF50;
  color: white;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-custom:hover {
  background-color: #45a049;
  border-color: #45a049;
}
</style>
