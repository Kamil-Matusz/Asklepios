<template>
  <v-container id="pdf-content">
    <v-row justify="center">
      <v-col cols="12" md="6">
        <v-card class="pa-5 elevation-10" color="#212121">
          <v-card-title>
            <h2 class="headline white--text">Wypis pacjenta ze szpitala</h2>
          </v-card-title>
          <v-card-subtitle class="mb-4 grey--text">Dane pacjenta:</v-card-subtitle>

          <v-row>
            <v-col cols="12" md="6">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Imię i nazwisko:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.patientName }} {{ dischargeDetails?.patientSurname }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
            <v-col cols="12" md="6">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">PESEL:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.peselNumber }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Adres:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.address }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
            <v-col cols="12" md="6">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Data wypisu:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.date }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Lekarz:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.doctorName }} {{ dischargeDetails?.doctorSurname }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Powód wypisu:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.dischargeReasson }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="subtitle-2 white--text font-weight-bold">Podsumowanie:</v-list-item-title>
                  <v-list-item-subtitle class="white--text">{{ dischargeDetails?.summary }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>

          <!-- Miejsce na podpis i pieczątkę -->
          <v-row class="mt-10 signature-section">
            <v-col cols="6" class="text-center">
              <div class="signature-line"></div>
              <div class="grey--text">Podpis lekarza</div>
            </v-col>
            <v-col cols="6" class="text-center">
              <div class="stamp-placeholder"></div>
              <div class="grey--text">Miejsce na pieczątkę</div>
            </v-col>
          </v-row>

          <v-card-actions class="mt-4">
            <v-spacer></v-spacer>
            <v-btn color="teal darken-4" dark @click="generatePDF">Generuj PDF</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useToast } from 'vue-toastification';
import { useDischargeStore } from '@/stores/dischargeStore';
import { type DischargeDetailsDto } from '@/models/Patients/discharge';
import html2pdf from 'html2pdf.js';

const dischargeStore = useDischargeStore();
const toast = useToast();
const route = useRoute();

const dischargeDetails = ref<DischargeDetailsDto | null>(null);

const getDischargeDetails = async (dischargeId: string) => {
  try {
    dischargeDetails.value = await dischargeStore.dispatchGetDischarge(dischargeId);
  } catch (error) {
    console.error('Error fetching discharge details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów wypisu');
  }
};

onMounted(() => {
  const dischargeId = route.params.id as string;
  getDischargeDetails(dischargeId);
});


const generatePDF = () => {
  const element = document.getElementById('pdf-content');
  const opt = {
    margin: [0, 0, 0, 0],
    filename: 'wypis_pacjenta.pdf',
    image: { type: 'jpeg', quality: 0.98 },
    html2canvas: { scale: 2, useCORS: true },
    jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
    pagebreak: { mode: ['avoid-all'] }
  };

  html2pdf().from(element).set(opt).save();
};

</script>

<style scoped>
.headline {
  font-weight: bold;
  color: #fff;
}

.signature-section {
  margin-top: 50px;
}

.signature-line {
  border-bottom: 1px solid #b0bec5;
  margin-bottom: 10px;
  width: 80%;
  margin-left: auto;
  margin-right: auto;
}

.stamp-placeholder {
  height: 100px;
  border: 1px dashed #b0bec5;
  margin-bottom: 10px;
  width: 80%;
  margin-left: auto;
  margin-right: auto;
}

.v-card {
  background-color: #212121;
  color: #eceff1;
}

#pdf-content {
  background-color: #212121;
  color: white;
  padding: 20mm;
  width: 210mm;
  height: 297mm;
  box-sizing: border-box;
}
</style>
