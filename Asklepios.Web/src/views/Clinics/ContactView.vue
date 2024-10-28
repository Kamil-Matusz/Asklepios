<template>
  <BasePage title="Informacje Kontaktowe">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title class="text-h5">Kontakt z Przychodnią</v-card-title>
            <v-card-text>
              <v-row>
                <v-col cols="12">
                  <p><strong>Nazwa:</strong> Asklepios</p>
                  <p><strong>Adres:</strong> ul. Cicha 24, 35-211 Rzeszów, Polska</p>
                  <p><strong>Telefon:</strong> +48 17 856 47 56</p>
                  <p><strong>Email:</strong> kontakt@asklepios.com</p>
                  <p><strong>Godziny otwarcia:</strong> Poniedziałek - Piątek, 8:00 - 16:00</p>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" @click="navigateBack">Powrót</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import BasePage from '@/components/pages/BasePage.vue';

const router = useRouter();

function navigateBack() {
  router.back();
}
</script>

<style scoped>
.v-card {
  background-color: white;
  color: black;
  padding: 20px;
}
.v-card-title {
  font-weight: bold;
  text-align: center;
  font-weight: 800;
  color: #2c3e50;
}

.v-card-text p {
  font-weight: 800;
}
</style>
