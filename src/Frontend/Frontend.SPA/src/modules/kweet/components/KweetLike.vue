<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { computed, defineComponent } from 'vue';
import { useStore } from 'vuex';

export default defineComponent({
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);

    return {};
  },
});
</script>

<template>
  <div></div>
</template>
