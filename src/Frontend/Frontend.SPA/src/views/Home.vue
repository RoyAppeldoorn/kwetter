<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  name: 'Home',
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);

    return { user };
  },
});
</script>

<template>
  <div>{{ user }}</div>
</template>
