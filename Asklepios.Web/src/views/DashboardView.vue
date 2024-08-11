<template>
  <BasePage title="Dashboard">
    <v-row v-if="isLoading">
      <v-col cols="12">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </v-col>
    </v-row>

    <v-row v-if="userRole === 'Admin' && !isLoading">
      <v-col cols="6">
        <BaseCardWithHover title="Ilość Oddziałów">
          <span class="text-h2">
            <AnimatedNumber :value="departmentStatsStore.totalDepartmentsCount" :duration="1500" />
            <span class="text-h5"> Oddziały</span>
          </span>
        </BaseCardWithHover>
      </v-col>

      <v-col cols="6">
        <BaseCardWithHover title="Całkowita Liczba Pacjentów">
          <span class="text-h2">
            <AnimatedNumber :value="departmentStatsStore.totalPatientsCount" :duration="1500" />
            <span class="text-h5"> Pacjentów</span>
          </span>
        </BaseCardWithHover>
      </v-col>
    </v-row>
  </BasePage>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { useJwtStore } from '@/stores/jwtStore';
import { useDepartmentStatsStore } from '@/stores/statisticsStore';
import BasePage from '@/components/pages/BasePage.vue';
import BaseCardWithHover from '@/components/cards/BaseCardWithHover.vue';
import AnimatedNumber from '@/components/animations/AnimatedNumber.vue';

const { getUserRole } = useJwtStore();
const userRole = ref<string | null>(null);

const departmentStatsStore = useDepartmentStatsStore();
const isLoading = ref(true);

onMounted(async () => {
  userRole.value = getUserRole();

  if (userRole.value === 'Admin') {
    await departmentStatsStore.dispatchGetTotalPatientsCount();
    await departmentStatsStore.dispatchGetTotalDepartmentsCount();
    await departmentStatsStore.dispatchGetAllDepartmentStats();

    console.log('Department Stats:', departmentStatsStore.departmentStats);
    console.log('Total Departments:', departmentStatsStore.totalDepartmentsCount);
    console.log('Total Patients:', departmentStatsStore.totalPatientsCount);
  }

  isLoading.value = false;
});
</script>
