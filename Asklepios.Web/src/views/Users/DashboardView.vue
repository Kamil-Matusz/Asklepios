<template>
  <BasePage :title="pageTitle">
    <v-row v-if="isLoading">
      <v-col cols="12">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </v-col>
    </v-row>

    <v-row v-if="(userRole === 'Admin' || userRole === 'IT Employee') && !isLoading" class="mb-4">
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

      <v-col cols="6">
        <BaseCardWithHover title="Dostępni lekarze">
          <span class="text-h2">
            <AnimatedNumber :value="departmentStatsStore.totalDoctorsCount" :duration="1500" />
          </span>
        </BaseCardWithHover>
      </v-col>

      <v-col cols="6">
        <BaseCardWithHover title="Dostępne pielęgniarki">
          <span class="text-h2">
            <AnimatedNumber :value="departmentStatsStore.totalNursesCount" :duration="1500" />
          </span>
        </BaseCardWithHover>
      </v-col>
    </v-row>

    <v-row v-if="(userRole === 'Admin' || userRole === 'IT Employee') && !isLoading" class="mb-4">
      <v-col cols="12">
        <BaseCardWithHover title="Dostępne operacje">
          <v-row>
            <v-col cols="6" class="text-center">
              <v-btn color="secondary" @click="goToRoute('allDischarges')" class="mx-2">
                Wypisy pacjentów
              </v-btn>
            </v-col>
            <v-col cols="6" class="text-center">
              <v-btn color="secondary" @click="goToRoute('patients')" class="mx-2">
                Pacjenci
              </v-btn>
            </v-col>
            <v-col cols="6" class="text-center">
              <v-btn color="secondary" @click="goToRoute('doctors')" class="mx-2">
                Lekarze
              </v-btn>
            </v-col>
            <v-col cols="6" class="text-center">
              <v-btn color="secondary" @click="goToRoute('nurses')" class="mx-2">
                Pielęgniarki
              </v-btn>
            </v-col>
            <v-col cols="6" class="text-center">
              <v-btn color="primary" @click="goToRoute('MonthlyDischarges')" class="mx-2">
                Statystyki wypisów
              </v-btn>
            </v-col>
            <v-col cols="6" class="text-center">
              <v-btn color="primary" @click="goToRoute('MonthlyAdmissions')" class="mx-2">
                Statystyki przyjęć
              </v-btn>
            </v-col>
          </v-row>
        </BaseCardWithHover>
      </v-col>
    </v-row>

    <v-row v-if="userRole === 'Doctor' && !isLoading" class="mb-4">
      <v-col cols="12">
        <BaseCardWithHover title="Dostępne akcje dla Lekarza">
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('yourDischarges')">
                Twoje Wypisy
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('yourPatients')">
                Twoi pacjenci
              </v-btn>
            </v-col>
          </v-row>
        </BaseCardWithHover>
      </v-col>
    </v-row>

    <v-row v-if="!isLoading && userRole === 'Admin' || userRole === 'Nurse' || userRole === 'Doctor'" class="mb-4">
      <v-col cols="12">
        <BaseCardWithHover title="Przychodnia - Moduł Zarządzania">
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('ClinicDashboard')">
                Zarządzanie przychodnią
              </v-btn>
            </v-col>
          </v-row>
        </BaseCardWithHover>
      </v-col>
    </v-row>

    <v-row v-if="!isLoading && userRole === 'Patient'" class="mb-4">
      <v-col cols="12">
        <BaseCardWithHover>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('ClinicDoctors')">
                Sprawdź lekarzy
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('UserPastAppointments')">
                Archiwalne wizyty
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('UserFutureAppointments')">
                Nadchodzące wizyty
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="green" @click="goToRoute('AddAppointmentByUser')">
                Umów się na konsultacje
              </v-btn>
            </v-col>
          </v-row>
        </BaseCardWithHover>
      </v-col>
    </v-row>
  </BasePage>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue';
import { useJwtStore } from '@/stores/jwtStore';
import { useDepartmentStatsStore } from '@/stores/statisticsStore';
import BasePage from '@/components/pages/BasePage.vue';
import BaseCardWithHover from '@/components/cards/BaseCardWithHover.vue';
import AnimatedNumber from '@/components/animations/AnimatedNumber.vue';
import { useRouter } from 'vue-router';

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
    await departmentStatsStore.dispatchGetTotalDoctorsCount();
    await departmentStatsStore.dispatchGetTotalNursesCount();
  }

  isLoading.value = false;
});

const pageTitle = computed(() => {
  if (userRole.value === 'Admin' || userRole.value === 'IT Employee') {
    return "Panel Kontrolny - Zarządzanie systemem";
  } else if (userRole.value === 'Doctor' || userRole.value === 'Nurse') {
    return "Panel Lekarza i Pielęgniarki - Dostępne akcje";
  } else if (userRole.value === 'Patient') {
    return "Akslepios Przychodnia - Twój panel";
  }
  return "";
});

const router = useRouter();
const goToRoute = (routeName: string) => {
  router.push({ name: routeName });
};
</script>

<style scoped>
.mb-4 {
  margin-bottom: 16px;
}

.BaseCardWithHover {
  background-color: gray;
}
</style>
