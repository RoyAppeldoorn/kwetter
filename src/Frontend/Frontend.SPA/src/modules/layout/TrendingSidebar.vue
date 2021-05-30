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
  <div class="relative hidden w-1/2 h-full md:block">
    <div class="hidden w-full bg-gray-800 rounded-2xl dark:bg-darkest lg:block">
      <div class="p-4">
        <p class="text-lg font-bold dark:text-gray-100">Who to follow</p>
      </div>
      <button
        v-for="trend in trends"
        :key="trend.name"
        class="flex w-full p-3 transition-colors duration-75 border-t hover:bg-lighter dark:hover:bg-darker border-lighter dark:border-dark focus:outline-none"
      >
        <div>
          <p class="text-sm font-bold leading-tight dark:text-gray-100">
            {{ trend.name }}
          </p>
        </div>
      </button>

      <button
        class="w-full p-4 text-left transition-colors duration-75 border-t hover:bg-lighter dark:hover:bg-darker text-red border-lighter dark:border-dark rounded-b-2xl focus:outline-none"
      >
        Show More
      </button>
    </div>
  </div>
</template>
