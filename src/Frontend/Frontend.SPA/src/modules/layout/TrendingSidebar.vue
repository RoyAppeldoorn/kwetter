<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'TrendingSidebar',
  setup() {
    return {
      trends: [{ name: '#test' }, { name: '#test2' }],
    };
  },
});
</script>

<template>
  <div class="md:block hidden w-1/3 h-full relative">
    <div class="w-full rounded-2xl bg-gray-800 dark:bg-darkest hidden lg:block">
      <div class="p-4">
        <p class="text-lg font-bold dark:text-gray-100">Trends</p>
      </div>
      <button
        v-for="trend in trends"
        :key="trend.name"
        class="w-full flex hover:bg-lighter dark:hover:bg-darker p-3 border-t border-lighter dark:border-dark focus:outline-none transition-colors duration-75"
      >
        <div>
          <p class="text-sm font-bold leading-tight dark:text-gray-100">
            {{ trend.name }}
          </p>
        </div>
      </button>

      <button
        class="p-4 w-full hover:bg-lighter dark:hover:bg-darker text-left text-red border-t border-lighter dark:border-dark rounded-b-2xl focus:outline-none transition-colors duration-75"
      >
        Show More
      </button>
    </div>
  </div>
</template>
