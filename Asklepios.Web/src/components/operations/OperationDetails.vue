<template>
  <v-dialog v-model="dialog" max-width="600px">
    <v-card>
      <v-card-title class="headline">Szczegóły Operacji</v-card-title>
      <v-card-text>
        <v-list dense>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Nazwa operacji: {{ operation.operationName }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Data rozpoczęcia: {{ new Date(operation.startDate).toLocaleString() }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Data zakończenia: {{ new Date(operation.endDate).toLocaleString() }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Typ znieczulenia: {{ operation.anesthesiaType }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Wynik: {{ operation.result }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Komplikacje: {{ operation.complications }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Lekarz: {{ operation.doctorName }} {{ operation.doctorSurname }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Pacjent: {{ operation.patientName }} {{ operation.patientSurname }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" text @click="dialog = false">Zamknij</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref, defineProps } from 'vue';

const props = defineProps<{ operation: OperationItemDto, dialog: boolean }>();

</script>

<style scoped>
.v-list-item {
  margin-bottom: 16px;
}
</style>
