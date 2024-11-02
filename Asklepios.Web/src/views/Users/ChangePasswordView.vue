<template>
  <BasePage title="Zmiana hasła">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card>
            <v-card-title>Zmiana hasła</v-card-title>
            <v-card-text>
              <v-text-field
                v-model="newPassword"
                label="Nowe hasło"
                type="password"
                prepend-icon="mdi-lock"
              ></v-text-field>
              <v-text-field
                v-model="confirmPassword"
                label="Potwierdź hasło"
                type="password"
                prepend-icon="mdi-lock"
              ></v-text-field>
              <v-alert v-if="errorMessage" type="error" dense>{{ errorMessage }}</v-alert>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" @click="changePassword">Zmień hasło</v-btn>
              <v-btn color="secondary" @click="navigateBack">Anuluj</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useUserStore } from '@/stores/userStore'
import { useJwtStore } from '@/stores/jwtStore'
import { useRouter } from 'vue-router'
import BasePage from '@/components/pages/BasePage.vue'

const userStore = useUserStore();
const jwtStore = useJwtStore();
const router = useRouter();

const newPassword = ref('');
const confirmPassword = ref('');
const errorMessage = ref('');

async function changePassword() {
  if (newPassword.value !== confirmPassword.value) {
    errorMessage.value = 'Hasła się nie zgadzają.';
    return;
  }

  try {
    await userStore.dispatchChangePassword(newPassword.value);
    await jwtStore.dispatchLogout();
    router.push('/');
  } catch (error) {
    console.error('Error changing password:', error);
    errorMessage.value = 'Nie udało się zmienić hasła. Spróbuj ponownie.';
  }
}

function navigateBack() {
  router.back();
}
</script>

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
