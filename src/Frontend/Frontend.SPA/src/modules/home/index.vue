<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent, reactive, ref } from 'vue';

export default defineComponent({
  name: 'Home',
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const kweet = ref('');
    const loading = ref(false);
    const input = reactive({ kweet });
    const inputEmpty = computed(() => input.kweet === '');
    return { user, loading, inputEmpty, postKweet };

    async function postKweet(): Promise<void> {
      loading.value = true;
    }
  },
});
</script>

<template>
  <div class="flex flex-col">
    <h1 class="text-xl font-bold dark:text-gray-100">Your feed</h1>
    <form class="w-full" @submit.prevent="postKweet">
      <div class="flex items-center border-b border-teal-500 py-2">
        <input
          v-model="input.kweet"
          class="appearance-none bg-transparent border-none w-full text-gray-100 mr-3 py-1 px-2 leading-tight focus:outline-none"
          type="text"
          placeholder="Whats up?"
          aria-label="Kweet"
        />
        <button class="flex-shrink-0 text-sm text-white py-2 px-4 rounded bg-red hover:bg-darkred" type="button">
          Kweet
        </button>
      </div>
    </form>
    <div>{{ user }}</div>
  </div>
</template>
