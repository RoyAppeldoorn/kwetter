<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'NavigationSidebarTab',
  props: {
    label: {
      type: String,
      required: true,
    },
    to: {
      type: String,
      required: true,
    },
  },
});
</script>

<template>
  <router-link :to="to">
    <button
      class="flex items-center py-2 mb-3 transition-colors duration-75 rounded-full md:pr-4 dark:hover:bg-darkred dark:hover:bg-opacity-20 hover:text-red focus:outline-none"
    >
      <slot name="icon"></slot>
      <span class="hidden text-lg font-bold text-left lg:block">
        {{ label }}
      </span>
    </button>
  </router-link>
</template>
