<script setup lang="ts">
import type { User } from '@/models/Users/user';
import { useJwtStore } from '@/stores/jwtStore';
import { onMounted, ref,} from 'vue';

const { dispatchLogout, getUserRole, getUser } = useJwtStore();

const user = ref({} as User);


onMounted(() => {
  getUserRole();
  // @ts-ignore
  user.value = getUser()
});
</script>

<template>
  <v-card style="z-index: 100">
    <v-layout>
      <v-navigation-drawer expand-on-hover rail theme="dark" class="bg-black">
        <v-list>
          <v-list-item
            prepend-icon="mdi-account"
            to="/profile"
          ></v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'IT Employee'"
            prepend-icon="mdi-view-dashboard"
            title="Panel Główny"
            to="/dashboard"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'IT Employee'"
            prepend-icon="mdi-account-multiple"
            title="Panel Użytkowników"
            to="/userManagment"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'IT Employee'"
            prepend-icon="mdi-book"
            title="Oddziały"
            to="/departments"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse'"
            prepend-icon="mdi mdi-head-plus"
            title="Spis badań"
            to="/examinations"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse'"
            prepend-icon="mdi mdi-heart-cog"
            title="Operacje"
            to="/operations"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse'"
            prepend-icon="mdi mdi-doctor"
            title="Lekarze"
            to="/doctors"
          ></v-list-item>
          <v-list-item
          v-if="getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse'"
            prepend-icon="mdi mdi-mother-nurse"
            title="Pielęgniarki"
            to="/nurses"
          ></v-list-item>
        </v-list>
        <template v-slot:append>
          <v-list-item prepend-icon="mdi-logout" @click="dispatchLogout" title="Wyloguj" class="my-6"></v-list-item>
        </template>
      </v-navigation-drawer>
    </v-layout>
  </v-card>
</template>
