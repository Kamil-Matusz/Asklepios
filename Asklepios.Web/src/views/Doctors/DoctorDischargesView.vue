<template>
  <BasePage title="Twoje wypisy">
    <v-data-table-server
      v-model:items-per-page="options.itemsPerPage"
      :headers="headers"
      :items="dischargeStore.discharges"
      :items-length="options.totalItems"
      :loading="options.loading"
      item-value="dischargeId"
      @update:options="handlePagination"
    >
      <template #item.actions="{ item }" dense>
        <v-btn
          @click="fetchDischargeDetails(item.dischargeId)"
          rounded="lg"
          size="small"
          color="primary"
          class="ml-2"
          icon="mdi-eye"
        ></v-btn>
      </template>
    </v-data-table-server>

    <v-dialog v-model="isDetailsDialogActive" max-width="800">
      <template #default>
        <v-card>
          <v-card-title>Szczegóły wypisu</v-card-title>
          <v-card-text id="discharge-details" class="pdf-content">
            <div class="header-section">
              <div class="title">Formularz wypisu ze szpitala</div>
              <div class="date">{{ dischargeDetails?.date }}</div>
            </div>
            <hr />

            <div class="section">
              <div class="section-title">Informacje podstawowe</div>
              <div class="section-content">
                <div><strong>Imię pacjenta:</strong> {{ dischargeDetails?.patientName }}</div>
                <div><strong>Nazwisko pacjenta:</strong> {{ dischargeDetails?.patientSurname }}</div>
                <div><strong>Pesel:</strong> {{ dischargeDetails?.peselNumber }}</div>
                <div><strong>Adres:</strong> {{ dischargeDetails?.address }}</div>
                <div><strong>Data wypisu:</strong> {{ dischargeDetails?.date }}</div>
              </div>
            </div>
            <hr />

            <div class="section">
              <div class="section-title">Informacje medyczne</div>
              <div class="section-content">
                <div><strong>Powód wypisu:</strong> {{ dischargeDetails?.dischargeReasson }}</div>
                <div><strong>Podsumowanie:</strong> {{ dischargeDetails?.summary }}</div>
                <div><strong>Imię lekarza:</strong> {{ dischargeDetails?.doctorName }}</div>
                <div><strong>Nazwisko lekarza:</strong> {{ dischargeDetails?.doctorSurname }}</div>
              </div>
            </div>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text="Zamknij" color="primary" rounded="lg" @click="isDetailsDialogActive = false"></v-btn>
            <v-btn text="Wyświetl PDF" color="secondary" rounded="lg" @click="generatePDF"></v-btn>
          </v-card-actions>
        </v-card>
      </template>
    </v-dialog>
  </BasePage>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useToast } from 'vue-toastification';
import BasePage from '@/components/pages/BasePage.vue';
import { useDischargeStore } from '@/stores/dischargeStore';
import { type DischargeDetailsDto } from '@/models/Patients/discharge';
import html2pdf from 'html2pdf.js';

const dischargeStore = useDischargeStore();
const toast = useToast();

const headers = [
  { title: 'Imię pacjenta', key: 'patientName', align: 'start' },
  { title: 'Nazwisko pacjenta', key: 'patientSurname', align: 'start' },
  { title: 'Pesel', key: 'peselNumber', align: 'start' },
  { title: 'Adres', key: 'address', align: 'start' },
  { title: 'Data wypisu', key: 'date', align: 'start' },
  { title: 'Akcje', key: 'actions', align: 'center', sortable: false }
];

const options = ref({
  itemsPerPage: 10,
  loading: true,
  totalItems: 0
});

const isDetailsDialogActive = ref(false);
const dischargeDetails = ref<DischargeDetailsDto | null>(null);

const getDischarges = async () => {
  options.value.loading = true;
  try {
    await dischargeStore.dispatchGetDoctorDischarges();
    options.value.totalItems = dischargeStore.totalItems;
  } catch (error) {
    console.error('Error fetching discharges:', error);
    toast.error('Wystąpił problem podczas pobierania wypisów');
  } finally {
    options.value.loading = false;
  }
};

const fetchDischargeDetails = async (id: string) => {
  try {
    dischargeDetails.value = await dischargeStore.dispatchGetDischarge(id);
    isDetailsDialogActive.value = true;
  } catch (error) {
    console.error('Error fetching discharge details:', error);
    toast.error('Wystąpił problem podczas pobierania szczegółów wypisu');
  }
};

const handlePagination = ({ page, itemsPerPage }: { page: number; itemsPerPage: number }) => {
  options.value.itemsPerPage = itemsPerPage;
  getDischarges();
};

const generatePDF = () => {
  const element = document.getElementById('discharge-details');

  // Set basic styles for the PDF content
  element.style.color = '#000';
  element.style.backgroundColor = '#fff';
  element.style.padding = '20px';
  element.style.fontSize = '14px';
  element.style.fontWeight = 'normal';

  const opt = {
    margin:       1,
    filename:     'discharge-details.pdf',
    image:        { type: 'jpeg', quality: 0.98 },
    html2canvas:  { scale: 2, useCORS: true },
    jsPDF:        { unit: 'in', format: 'letter', orientation: 'portrait' }
  };

  // Generate PDF and open it in a new window
  html2pdf().from(element).set(opt).outputPdf('datauristring').then((pdfUri) => {
    const pdfWindow = window.open("");
    pdfWindow?.document.write(
      `<iframe width='100%' height='100%' src='${pdfUri}'></iframe>`
    );
  });
};

onMounted(() => {
  getDischarges();
});
</script>

<style scoped>
.pdf-content {
  font-family: Arial, sans-serif;
  font-size: 14px;
  color: #333;
  background-color: #fff;
  padding: 20px;
}

.header-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #f5f5f5;
  padding: 10px;
  border-bottom: 2px solid #000;
}

.header-section .title {
  font-size: 20px;
  font-weight: bold;
}

.header-section .date {
  font-size: 12px;
  color: #555;
}

.section-title {
  font-size: 16px;
  font-weight: bold;
  margin-bottom: 10px;
  padding-bottom: 5px;
  border-bottom: 1px solid #ddd;
}

.section-content {
  margin-bottom: 20px;
}

hr {
  border: none;
  border-top: 2px solid #000;
  margin: 20px 0;
}
</style>
