<template>
  <span>{{ animatedValue }}</span>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted } from 'vue';

interface Props {
  value: number;
  duration: number;
}

const props = defineProps<Props>();
const animatedValue = ref(0);

const animateNumber = (start: number, end: number, duration: number) => {
  const startTime = performance.now();
  const animate = (currentTime: number) => {
    const progress = Math.min((currentTime - startTime) / duration, 1);
    animatedValue.value = Math.floor(progress * (end - start) + start);
    if (progress < 1) {
      requestAnimationFrame(animate);
    }
  };
  requestAnimationFrame(animate);
};

onMounted(() => {
  animateNumber(0, props.value, props.duration);
});

watch(() => props.value, (newValue) => {
  animateNumber(animatedValue.value, newValue, props.duration);
});
</script>
