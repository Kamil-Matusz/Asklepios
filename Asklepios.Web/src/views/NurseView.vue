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
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useNurseStore } from '@/stores/nurseStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';

const nurseStore = useNurseStore();
const toast = useToast();
const router = useRouter();

const nurses = ref([]);

const getNurses = async () => {
  try {
    await nurseStore.dispatchGetNurses({ pageIndex: 1, pageSize: 10 });
    nurses.value = nurseStore.nurses;
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
</script>
