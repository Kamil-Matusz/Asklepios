<script setup lang="ts">
import BasePage from '@/components/pages/BasePage.vue'
import { onMounted, ref } from 'vue'
import { type User } from '@/models/Users/user'
import { useJwtStore } from '@/stores/jwtStore'

const user = ref<User | null>(null)
const jwtStore = useJwtStore();


/*const handleChangePassword = () => {
  if (user.value) {
    userStore.dispatchChangePassword(dataToPasswordChange.value, user.value.userId).then(() => {
      toast.success('Pomyślnie zmieniono hasło!')
      dataToPasswordChange.value = new InputPasswordChange()
    }).catch(() => {
      toast.error('Wystąpił błąd podczas zmiany hasła.')
    })
  }
}*/

onMounted(() => {
  user.value = jwtStore.getUser()
  console.log(user.value);
})
</script>

<template>
  <BasePage title="Twój profil">
    <v-row>
      <v-col cols="12" lg="6">
        <v-card
          class="mb-5"
          variant="outlined"
          title="Twoje dane"
          subtitle="Poniżej znajdują się dane Twojego konta."
          cols="12"
          lg="6"
        >
          <v-list density="compact" nav>
            <v-list-item prepend-icon="mdi-pound" title="Identyfikator"> {{ user?.userId }} </v-list-item>
            <v-list-item prepend-icon="mdi-email" title="Email"> {{ user?.email }} </v-list-item>
            <v-list-item prepend-icon="mdi-tie" title="Rola"> {{ jwtStore.getUserRole() }} </v-list-item>
          </v-list>
        </v-card>
      </v-col>

      <v-col cols="12" lg="6">
        <v-card
          class="mb-5"
          variant="outlined"
          title="Zmiana hasła"
          subtitle="Poniżej możesz zmienić hasło do swojego konta."
          cols="12"
          lg="6"
        >
          <v-dialog max-width="500">
            <template v-slot:activator="{ props: activatorProps }">
              <v-btn
                v-bind="activatorProps"
                color="primary"
                variant="flat"
                class="mb-4 ml-4"
                style="width: 15rem"
                >Zmień hasło</v-btn>
            </template>

          </v-dialog>
        </v-card>
      </v-col>
    </v-row>
  </BasePage>
</template>
