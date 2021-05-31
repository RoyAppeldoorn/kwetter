<template>
  <div class="flex justify-between mb-4">
    <div class="flex items-center">
      <div class="w-12 h-12 mr-6 bg-white rounded-full" />
      <div>
        <p class="leading-none text-white">{{ follower.username }}</p>
        <p class="text-gray-600">@{{ follower.username }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { Follower } from '../types';

export default defineComponent({
  name: 'FollowCard',
  props: {
    follower: {
      type: Object as PropType<Follower>,
      required: true,
    },
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
