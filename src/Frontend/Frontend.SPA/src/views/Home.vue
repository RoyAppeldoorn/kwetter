<script lang="ts">
import { useStore } from '@/store';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Home',
  setup() {
    const store = useStore();

    const user = store.getters['GET_USER'];
    return { user };
  },
});
</script>

<template>
  <div>{{ user }}</div>
</template>
