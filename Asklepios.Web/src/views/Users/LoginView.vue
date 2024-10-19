<script setup lang="ts">
import { InputLoginData } from '@/models/authorization';
import { useJwtStore } from '@/stores/jwtStore';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const jwtStore = useJwtStore();
const loginData = ref(new InputLoginData());
const router = useRouter();

const handleLogin = () => {
  jwtStore.dispatchLogin(loginData.value);
}

const goToRegister = () => {
  router.push('/signUpToClinic');
}
</script>

<template>
  <v-container class="d-flex align-center justify-center" fluid>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <h1 class="text-center">Asklepios - System Zarządzania</h1>
        <v-card class="elevation-10">
          <v-card-title class="headline text-center">Logowanie</v-card-title>
          <v-card-text>
            <v-form class="text-center">
              <v-text-field
                v-model="loginData.email"
                label="E-mail użytkownika"
                outlined
                dense
                :rules="[v => !!v || 'E-mail jest wymagany']"
                prepend-inner-icon="mdi-email"
              ></v-text-field>
              <v-text-field
                v-model="loginData.password"
                label="Hasło"
                type="password"
                outlined
                dense
                :rules="[v => !!v || 'Hasło jest wymagane']"
                prepend-inner-icon="mdi-lock"
              ></v-text-field>
              <v-btn color="green" @click.prevent="handleLogin" class="mt-4" block>
                Zaloguj
              </v-btn>
              <v-btn color="blue" @click="goToRegister" class="mt-4" block>
                Panel rejestracji
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
