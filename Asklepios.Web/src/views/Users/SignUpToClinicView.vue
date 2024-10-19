<script setup lang="ts">
import { InputLoginData } from '@/models/authorization';
import { useUserStore } from '@/stores/userStore';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const userStore = useUserStore();
const registrationData = ref(new InputLoginData());
const router = useRouter();

const handleRegister = () => {
  userStore.dispatchCreateUserClinic(registrationData.value);
}

const goToLogin = () => {
  router.push('/');
}
</script>

<template>
  <v-container class="d-flex align-center justify-center" fluid>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <h1 class="text-center">Asklepios - System Zarządzania</h1>
        <v-card class="elevation-10">
          <v-card-title class="headline text-center">Rejestracja</v-card-title>
          <v-card-text>
            <v-form class="text-center">
              <v-text-field
                v-model="registrationData.email"
                label="E-mail użytkownika"
                outlined
                dense
                :rules="[v => !!v || 'E-mail jest wymagany']"
                prepend-inner-icon="mdi-email"
              ></v-text-field>
              <v-text-field
                v-model="registrationData.password"
                label="Hasło"
                type="password"
                outlined
                dense
                :rules="[v => !!v || 'Hasło jest wymagane']"
                prepend-inner-icon="mdi-lock"
              ></v-text-field>
              <v-text-field
                v-model="registrationData.confirmPassword"
                label="Potwierdź hasło"
                type="password"
                outlined
                dense
                :rules="[v => !!v || 'Potwierdzenie hasła jest wymagane']"
                prepend-inner-icon="mdi-lock-check"
              ></v-text-field>
              <v-btn color="green" @click.prevent="handleRegister" class="mt-4" block>
                Zarejestruj się
              </v-btn>
              <v-btn color="blue" @click="goToLogin" class="mt-4" block>
                Panel logowania
              </v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
h1 {
  color: #2c3e50;
  margin-bottom: 20px;
}

.v-card {
  border-radius: 16px;
  background-color: #757575;
}

.v-text-field {
  margin-bottom: 16px;
}

.v-btn {
  font-weight: bold;
}
</style>
