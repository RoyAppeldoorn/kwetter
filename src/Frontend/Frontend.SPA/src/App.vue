<template>
  <div class="mx-auto max-w-7xl">
    <Header />
    <div class="flex px-2 sm:px-6 lg:px-8">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Header from '@/components/Header.vue';
export default defineComponent({
  components: {
    Header,
  },
});
</script>

<style lang="postcss">
body {
  @apply bg-gray-900 text-white;
}
</style>
