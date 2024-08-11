<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useJwtStore } from '@/stores/jwtStore';
import { useDepartmentStatsStore } from '@/stores/statisticsStore';
import BasePage from '@/components/pages/BasePage.vue';
import BaseCardWithHover from '@/components/cards/BaseCardWithHover.vue';
import Vue3Odometer from 'vue3-odometer';
import 'odometer/themes/odometer-theme-default.css';

const { getUserRole } = useJwtStore();
const userRole = ref<string | null>(null);

const departmentStatsStore = useDepartmentStatsStore();
const isLoading = ref(true);

onMounted(async () => {
  userRole.value = getUserRole();

  if (userRole.value === 'Admin') {
    await departmentStatsStore.dispatchGetTotalPatientsCount();
    await departmentStatsStore.dispatchGetAllDepartmentStats();
  }

  isLoading.value = false;
});
</script>

<template>
  <BasePage title="Dashboard">
    <!-- Wczytywanie danych -->
    <v-row v-if="isLoading">
      <v-col cols="12">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </v-col>
    </v-row>

    <v-row v-if="userRole === 'Admin' && !isLoading">
      <!-- Karta 1: Statystyki oddziałów -->
      <v-col cols="6">
        <BaseCardWithHover title="Ilość Pacjentów w Oddziałach">
          <span class="text-h2">
            <Vue3Odometer :value="departmentStatsStore.departmentStats?.totalDepartments" format="(,ddd)"></Vue3Odometer>
            <span class="text-h5">Pacjentów</span>
          </span>
        </BaseCardWithHover>
      </v-col>

      <v-col cols="6">
        <BaseCardWithHover title="Całkowita Liczba Pacjentów">
          <span class="text-h2">
            <Vue3Odometer :value="departmentStatsStore.totalPatients" format="(,ddd)"></Vue3Odometer>
            <span class="text-h5">Pacjentów</span>
          </span>
        </BaseCardWithHover>
      </v-col>
    </v-row>
  </BasePage>
</template>

<style lang="scss">
.sparkline {
  animation: draw 2s;
  height: 10rem;
}

@keyframes draw {
  0% {
    transform: scaleY(0);
  }

  100% {
    transform: scaleY(1);
  }
}
</style>
