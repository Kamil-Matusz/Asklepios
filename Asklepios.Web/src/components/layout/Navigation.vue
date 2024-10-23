<script setup lang="ts">
import type { User } from '@/models/Users/user';
import { useJwtStore } from '@/stores/jwtStore';
import { onMounted, ref } from 'vue';

const { dispatchLogout, getUserRole, getUser } = useJwtStore();

const user = ref<User | null>(null);

onMounted(() => {
  user.value = getUser();
});
</script>

<template>
  <v-card v-if="user" style="z-index: 100">
    <v-layout>
      <v-navigation-drawer expand-on-hover rail theme="light" class="bg-light-grey">
        <v-list>
          <v-list-item
            prepend-icon="mdi-account"
            to="/profile"
          ></v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi-view-dashboard"
            title="Panel Główny"
            to="/dashboard"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi-account-multiple"
            title="Panel Użytkowników"
            to="/userManagment"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi-book"
            title="Oddziały"
            to="/departments"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi-book"
            title="Pokoje w Oddziałach"
            to="/rooms"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse')"
            prepend-icon="mdi mdi-head-plus"
            title="Spis badań"
            to="/examinations"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse')"
            prepend-icon="mdi mdi-heart-cog"
            title="Operacje"
            to="/operations"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi mdi-doctor"
            title="Lekarze"
            to="/doctors"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse')"
            prepend-icon="mdi mdi-mother-nurse"
            title="Pielęgniarki"
            to="/nurses"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse')"
            prepend-icon="mdi mdi-card-search-outline"
            title="Wyszukaj historię pacjeta"
            to="/patientHistory"
          ></v-list-item>
          <v-list-item
            v-if="user && getUserRole() === 'Admin' || getUserRole() === 'Doctor'"
            prepend-icon="mdi mdi-account-injury"
            title="Pacjenci"
            to="/patients"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse')"
            prepend-icon="mdi mdi-ambulance"
            title="Dodaj wynik badania"
            to="/addExamResult"
          ></v-list-item>
        </v-list>
        <v-list-item
            v-if="user && (getUserRole() === 'Patient')"
            prepend-icon="mdi mdi-home-account"
            title="Panel główny"
            to="/dashboard"
          ></v-list-item>
        <v-list-item
            v-if="user && (getUserRole() === 'Patient')"
            prepend-icon="mdi mdi-ambulance"
            title="Przychodnia"
            to="/clinicDoctors"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Patient')"
            prepend-icon="mdi mdi-calendar-account"
            title="Twoje wizyty"
            to="/userPastAppointments"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Patient')"
            prepend-icon="mdi mdi-plus"
            title="Umów się do lekarza"
            to="/addAppointmentByUser"
          ></v-list-item>
          <v-list-item
            v-if="user && (getUserRole() === 'Patient')"
            prepend-icon="mdi mdi-calendar-blank-multiple"
            title="Nadchodzące wizyty"
            to="/userFutureAppointments"
          ></v-list-item>
        <template v-slot:append>
          <v-list-item v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'Patient')" prepend-icon="mdi-logout" @click="dispatchLogout" title="Wyloguj" class="my-6"></v-list-item>
        </template>
      </v-navigation-drawer>
    </v-layout>
  </v-card>
</template>
