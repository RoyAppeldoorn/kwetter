<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'TrendingSidebar',
  setup() {
    return {
      trends: [{ name: '@roy' }, { name: '@beau' }],
    };
  },
});
</script>

<template>
  <div class="relative hidden w-1/2 h-full md:block">
    <div class="hidden w-full bg-gray-800 rounded-xl lg:block">
      <div class="p-4">
        <p class="text-lg font-bold">Who to follow</p>
      </div>
      <button
        v-for="trend in trends"
        :key="trend.name"
        class="flex w-full px-3 py-4 border-t border-gray-700 hover:bg-gray-800 focus:outline-none"
      >
        <div>
          <p class="text-sm leading-tight dark:text-gray-100">
            {{ trend.name }}
          </p>
        </div>
      </button>

      <button
        class="w-full p-4 font-bold text-left text-white bg-gray-700 border-t border-gray-700 hover:bg-red rounded-b-2xl focus:outline-none"
      >
        Show More
      </button>
    </div>
  </div>
</template>
