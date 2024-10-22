<script setup lang="ts">
import BasePage from '@/components/pages/BasePage.vue';
import { onMounted } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { useRouter } from 'vue-router';

const userStore = useUserStore();
const router = useRouter();

const goToRoute = (routeName: string) => {
  router.push({ name: routeName });
};

function formatDate(date: Date): string {
  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit'
  };
  return date.toLocaleDateString(undefined, options);
}

const translateRole = (role: string) => {
  const roleTranslations: { [key: string]: string } = {
    'Admin': 'Administrator',
    'Doctor': 'Lekarz',
    'Nurse': 'Pielęgniarka',
    'IT Employee': 'Pracownik IT',
    'Patient': 'Pacjent'
  };
  return roleTranslations[role] || role;
};

onMounted(() => {
  userStore.fetchCurrentUser();
});
</script>

<template>
  <BasePage title="Twój profil">
    <v-row>
      <v-col cols="12" lg="6">
        <v-card
          class="mb-5"
          variant="outlined"
          title="Twoje dane"
          subtitle="Poniżej znajdują się dane Twojego konta.">
          <v-list density="compact" nav>
            <v-list-item prepend-icon="mdi-email" title="Email"> {{ userStore.currentUser?.email }} </v-list-item>
            <v-list-item
            v-if="user && (getUserRole() === 'Admin' || getUserRole() === 'Doctor' || getUserRole() === 'Nurse' || getUserRole() === 'IT Employee')"
            prepend-icon="mdi-tie" title="Rola"> {{ translateRole(userStore.currentUser?.role) }} </v-list-item>
            <v-list-item prepend-icon="mdi-calendar-account" title="Konto stworzone"> {{ userStore.currentUser?.createdAt ? formatDate(new Date(userStore.currentUser.createdAt)) : '' }} </v-list-item>
            <v-list-item prepend-icon="mdi-account-check" title="Status konta"> {{ userStore.currentUser?.isActive ? 'Aktywne' : 'Nieaktywne' }} </v-list-item>
          </v-list>
        </v-card>
      </v-col>

      <v-col cols="12" lg="6">
        <v-card
          class="mb-5"
          variant="outlined"
          title="Zmiana hasła"
          subtitle="Poniżej możesz zmienić hasło do swojego konta.">
          <v-btn
            @click="goToRoute('changePassword')"
            color="primary"
            variant="flat"
            class="mb-4 ml-4"
            style="width: 15rem"
          >Zmień hasło</v-btn>
        </v-card>
      </v-col>
    </v-row>
  </BasePage>
</template>

<style scoped>
.v-card {
  background-color: #616161;
}

.v-list {
  background-color: #616161;
}
</style>
