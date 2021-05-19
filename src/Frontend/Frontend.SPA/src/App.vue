<template>
  <div id="app">
    <div class="mx-auto max-w-7xl">
      <Header v-if="authenticated" />
      <div class="flex px-2 sm:px-6 lg:px-8">
        <router-view />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import Header from '@/components/Header.vue';
import { rootStore } from './store';
import { AuthGetterTypes } from './modules/auth/store/auth.getters';

export default defineComponent({
  components: {
    Header,
  },
  setup() {
    const authenticated = computed(() => rootStore.getters[AuthGetterTypes.GET_IS_LOGGED_IN]);

    return {
      authenticated,
    };
  },
});
</script>

<style lang="postcss">
body {
  @apply bg-gray-900 text-white;
}
</style>
