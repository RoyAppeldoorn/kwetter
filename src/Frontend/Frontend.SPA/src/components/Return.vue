<script lang="ts">
import { defineComponent } from 'vue';
import IconArrowLeft from '@/icons/IconArrowLeft.vue';

export default defineComponent({
  components: { IconArrowLeft },
  name: 'Return',
  props: {
    to: {
      type: String,
      default: '/home',
    },
  },
});
</script>

<template>
  <router-link class="focus:outline-none" :to="to">
    <IconArrowLeft :size="24" class="text-blue" />
  </router-link>
</template>
