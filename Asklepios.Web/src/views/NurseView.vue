<template>
  <BasePage title="Zarządzanie pielęgniarkami">
    <v-container>
      <v-row>
        <v-col v-for="nurse in nurses" :key="nurse.nurseId" cols="12" md="6" lg="4">
          <v-card>
            <v-card-title>{{ nurse.name }} {{ nurse.surname }}</v-card-title>
            <v-card-text>
              <div>Oddział: {{ nurse.departmentName }}</div>
            </v-card-text>
            <v-card-actions>
              <v-btn color="blue" text @click="goToDetails(nurse.nurseId)">Szczegóły</v-btn>
              <v-btn color="red" text @click="deleteNurse(nurse.nurseId)">Usuń</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12">
          <v-select
            v-model="itemsPerPage"
            :items="itemsPerPageOptions"
            label="Liczba obiektów na stronę"
            @change="getNurses"
          ></v-select>
        </v-col>
      </v-row>

      <v-pagination
        v-model="currentPage"
        :length="Math.ceil(totalNurses / itemsPerPage)"
        @input="getNurses"
        color="blue"
      ></v-pagination>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { useNurseStore } from '@/stores/nurseStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';

const nurseStore = useNurseStore();
const toast = useToast();
const router = useRouter();

const currentPage = ref(1);
const itemsPerPage = ref(10);
const itemsPerPageOptions = ref([5, 10, 20, 50]);

const nurses = ref([]);
const totalNurses = ref(0);

const getNurses = async () => {
  try {
    const paginationParams = {
      pageIndex: currentPage.value,
      pageSize: itemsPerPage.value
    };
    await nurseStore.dispatchGetNurses(paginationParams);
    nurses.value = nurseStore.nurses;
    totalNurses.value = nurseStore.totalItems;
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania danych pielęgniarek');
  }
};

const deleteNurse = async (id: string) => {
  try {
    await nurseStore.dispatchDeleteNurse(id);
    toast.success('Pomyślnie usunięto pielęgniarkę');
    getNurses();
  } catch (error) {
    toast.error('Wystąpił problem podczas usuwania pielęgniarki');
  }
};

const goToDetails = (id: string) => {
  router.push(`/nurse/${id}`);
};

onMounted(getNurses);

watch([currentPage, itemsPerPage], getNurses);
</script>

<style scoped>
.doctor-card {
  margin-bottom: 20px;
}
</style>
