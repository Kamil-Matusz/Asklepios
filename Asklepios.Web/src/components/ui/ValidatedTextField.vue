<script setup lang="ts">
import { defineProps } from 'vue';

defineProps<{
  errorMessage: string,
  isNumber?: boolean
}>();
</script>

<template>
    <v-text-field
        density="compact"
        variant="outlined"
        :error-messages="[props.errorMessage]"
        required
        v-bind="$props"
    ></v-text-field>
</template>
