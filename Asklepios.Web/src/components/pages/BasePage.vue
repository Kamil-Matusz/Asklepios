<script setup lang="ts">

defineProps<{
  title: string
}>()
</script>

<template>
  <div class="ml-14 h-screen pa-8 pb-12 d-flex flex-column">
    <h2 class="text-h4 text-white my-4 text-left">{{ title }}</h2>
    <slot name="above-card"></slot>
        <slot></slot>
  </div>
</template>

<style>
.hide-scrollbar::-webkit-scrollbar {
  display: none;
}
</style>
