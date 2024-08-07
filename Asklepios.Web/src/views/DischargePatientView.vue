<template>
  <BasePage title="Wypisanie pacjenta">
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>
              Wypisanie pacjenta
            </v-card-title>
            <v-card-text>
              <v-form @submit.prevent="handleSubmit">
                <v-text-field
                  v-model="form.dischargeReasson"
                  label="Powód wypisu"
                  :rules="dischargeReassonRules"
                  required
                ></v-text-field>
                <v-text-field
                  v-model="form.summary"
                  label="Podsumowanie wypisu"
                  :rules="summaryRules"
                  required
                ></v-text-field>
                <v-btn type="submit" color="green">Wypisz pacjenta</v-btn>
              </v-form>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useDischargeStore } from '@/stores/dischargeStore';
import { useToast } from 'vue-toastification';
import { DischargePersonDto } from '@/models/Patients/discharge';

const route = useRoute();
const router = useRouter();
const toast = useToast();
const dischargeStore = useDischargeStore();

const form = ref<DischargePersonDto>({
  patientId: '',
  dischargeReasson: '',
  summary: '',
});

const dischargeReassonRules = [(v: string) => !!v || 'Powód wypisu jest wymagany'];
const summaryRules = [(v: string) => !!v || 'Podsumowanie jest wymagane'];

onMounted(() => {
  const patientId = route.params.id as string;
  form.value.patientId = patientId;
});

const handleSubmit = async () => {
  try {
    await dischargeStore.dispatchDischargePatient(form.value);
    toast.success('Pacjent został pomyślnie wypisany');
    router.push({ name: 'patients' });
  } catch (error) {
    console.error('Error discharging patient:', error);
    toast.error('Wystąpił problem podczas wypisywania pacjenta');
  }
};
</script>
