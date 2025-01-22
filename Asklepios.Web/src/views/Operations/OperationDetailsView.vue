<template>
  <BasePage title="Szczegóły operacji">
    <div v-if="operation" class="operation-details">
      <h2>{{ operation.operationName }}</h2>
      <p><strong>Data rozpoczęcia:</strong> {{ operation.startDate }}</p>
      <p><strong>Data zakończenia:</strong> {{ operation.endDate }}</p>
      <p><strong>Typ znieczulenia:</strong> {{ operation.anesthesiaType }}</p>
      <p><strong>Wynik:</strong> {{ operation.result }}</p>
      <p><strong>Komplikacje:</strong> {{ operation.complications }}</p>
      <p><strong>Lekarz:</strong> {{ operation.doctorName }} {{ operation.doctornSurname }}</p>
      <p><strong>Pacjent:</strong> {{ operation.patientName }} {{ operation.patientSurname }}</p>

      <button @click="goBack" class="btn btn-custom">Powrót</button>
    </div>
  </BasePage>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useOperationStore } from '@/stores/operationStore';
import { useToast } from 'vue-toastification';
import { OperationItemDto } from '@/models/Examinations/operation';

const route = useRoute();
const router = useRouter();
const operationStore = useOperationStore();
const toast = useToast();

const operation = ref<OperationItemDto | null>(null);

const fetchOperationDetails = async () => {
  try {
    const operationId = route.params.id as string;
    await operationStore.dispatchGetOperation(operationId);
    operation.value = operationStore.operationInfo;
  } catch (error) {
    toast.error('Wystąpił problem podczas pobierania szczegółów operacji');
  }
};

const goBack = () => {
  router.go(-1);
};

onMounted(fetchOperationDetails);
</script>

<style scoped>
.operation-details {
  margin: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  color: black;
}

.operation-details h2 {
  margin-bottom: 10px;
}

.operation-details p {
  margin-bottom: 5px;
}

.btn-custom {
  margin-top: 15px;
  padding: 10px 20px;
  border: 2px solid #4CAF50;
  border-radius: 5px;
  background-color: #4CAF50;
  color: white;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-custom:hover {
  background-color: #45a049;
  border-color: #45a049;
}
</style>
