<script lang="ts">
import Header from '@/components/Header.vue';
import { defineComponent } from 'vue';
import NavigationSidebar from './NavigationSidebar.vue';
import TrendingSidebar from './TrendingSidebar.vue';

export default defineComponent({
  components: { NavigationSidebar, TrendingSidebar, Header },
  name: 'BaseLayout',
});
</script>

<template>
  <div class="mx-auto max-w-7xl">
    <Header />
    <div class="flex px-2 sm:px-6 lg:px-8">
      <NavigationSidebar />
      <router-view class="px-12" />
      <TrendingSidebar />
    </div>
  </div>
</template>
