<template>
  <div class="max-w-7xl mx-auto">
    <div id="nav">
      <Header />
    </div>
    <router-view />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Header from '@/components/Header.vue';

export default defineComponent({
  components: {
    Header
  }
});
</script>

<style lang="postcss">
body {
  @apply bg-gray-900 text-white;
}

#nav a {
  font-weight: bold;
  @apply text-gray-600;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
