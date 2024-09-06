<template>
  <v-container class="d-flex flex-column align-center justify-center" style="height: 100vh;">
    <v-card class="text-center" elevation="10" style="padding: 50px; max-width: 600px;">
      <v-card-title class="headline">Błąd 404</v-card-title>
      <v-card-text>
        <p>Przepraszamy, ale strona, której szukasz, nie istnieje.</p>
      </v-card-text>
      <v-card-actions class="justify-center">
        <v-btn color="primary" @click="goHome">Powrót do strony głównej</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter()

const goHome = () => {
  router.push('/')
}
</script>

<style scoped>
.v-card {
  background-color: var(--v-surface);
  color: var(--v-on-surface);
}
.headline {
  font-weight: bold;
  margin-bottom: 20px;
}
p {
  font-size: 1.2rem;
  margin-bottom: 30px;
}
</style>
