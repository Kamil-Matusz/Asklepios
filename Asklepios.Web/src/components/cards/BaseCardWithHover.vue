<script setup lang="ts">

const props = defineProps<{
    overflowHidden?: boolean
    noPadding?: boolean
    title?: string
    subtitle?: string
}>();

const emit = defineEmits(['click']);

</script>

<template>
    <div :class="`magicCard ${props.noPadding ? 'no-padding' : ''}`" @click="emit('click')">
        <div :class="`magicCard-content ${props.overflowHidden ? 'overflow-hidden' : ''}`">
            <h6 v-if="props.title" class="text-h6 side-line mb-4" color="primary">
                {{ props.title }}
            </h6>
            <p class="text-body mb-4" v-if="props.subtitle">{{ props.subtitle }}</p>
            <slot></slot>
        </div>
    </div>
</template>


<style>
:root {
    --bg-color: ;
    --card-color: rgb(23, 23, 23);
}

.no-padding {
    padding: 0rem !important;
}

.card-container:hover .magicCard::after {
    opacity: 1;
}

.magicCard:hover::before {
    opacity: .5;
}

.magicCard {
    background-color: gray;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
    position: relative !important;
    padding: 2px;
}

.magicCard::before,
.magicCard::after {
    border-radius: inherit;
    content: "";
    height: 100%;
    left: 0px;
    opacity: 0;
    position: absolute;
    top: 0px;
    transition: opacity 500ms;
    width: 100%;
}

.magicCard::before {
    background: radial-gradient(800px circle at var(--mouse-x) var(--mouse-y),
            rgba(255, 255, 255, 0.04),
            transparent 40%);
    z-index: 3;
    pointer-events: none;
}

.magicCard::after {

    background: radial-gradient(600px circle at var(--mouse-x) var(--mouse-y),
            rgba(255, 255, 255, 0.4),
            transparent 40%);
    z-index: 1;
}

.magicCard>.magicCard-content {
    background-color: #a9a9a9;
    border-radius: inherit;
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    inset: 1px;
    padding: 1rem;
    z-index: 2;
    overflow: auto;
}

.magicCard-content>* {
    pointer-events: auto;
}
</style>
