<template>
  <BasePage title="Panel Kontrolny - Zarządzanie Przychodnią">
    <v-row v-if="isLoading">
      <v-col cols="12">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </v-col>
    </v-row>

    <v-row v-if="(userRole === 'Nurse' || userRole === 'Doctor' || userRole === 'Admin' || userRole === 'IT Employee') && !isLoading">
      <v-col cols="12">
        <BaseCardWithHover title="Dostępne opcje i formularze">
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('TodayAppointments')">
                Wizyty zaplanowane na dziś
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('DayAppointments')">
                Wyszukaj wizyty na dany dzień
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('AddAppointment')">
                Zapisz nową wizytę
              </v-btn>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="text-center">
              <v-btn color="secondary" @click="goToRoute('ClinicPatients')">
                Informacje o pacjentach
              </v-btn>
            </v-col>
          </v-row>
        </BaseCardWithHover>
      </v-col>
    </v-row>

  </BasePage>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { useJwtStore } from '@/stores/jwtStore';
import BasePage from '@/components/pages/BasePage.vue';
import BaseCardWithHover from '@/components/cards/BaseCardWithHover.vue';
import { useRouter } from 'vue-router';

const { getUserRole } = useJwtStore();
const userRole = ref<string | null>(null);

const isLoading = ref(true);

onMounted(async () => {
  userRole.value = getUserRole();
  isLoading.value = false;
});

const router = useRouter();
const goToRoute = (routeName: string) => {
  router.push({ name: routeName });
};
</script>
