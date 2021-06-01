<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent, reactive, ref } from 'vue';
import { KweetActionTypes } from '../kweet/store/kweet.actions';
import CreateKweetCommand from '../kweet/dto/CreateKweetCommand';
import { User } from '../auth/types';
import Guid from '@/utils/guid';

export default defineComponent({
  name: 'Home',
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const kweet = ref('');
    const kweetMaxCharacters = ref(140);
    const remainingKweetCharacters = ref('Remaining 140 characters');
    const input = reactive({ kweet });
    const inputEmpty = computed(() => input.kweet === '');
    return { user, inputEmpty, postKweet, input, remainingKweetCharacters, remainingCharCount };

    async function postKweet(): Promise<void> {
      const user: User | null = store.getters[AuthGetterTypes.GET_USER];
      if (user) {
        const kweetId = Guid.newGuid().toString();

        const command: CreateKweetCommand = {
          kweetId: kweetId,
          userId: user.userId,
          message: input.kweet,
        };

        store.dispatch(KweetActionTypes.POST_KWEET, command);
      } else {
        console.log('user is not set correctly');
      }
    }

    function remainingCharCount() {
      if (input.kweet.length > kweetMaxCharacters.value) {
        input.kweet = input.kweet.substring(0, kweetMaxCharacters.value);
      } else {
        var remainCharacters = kweetMaxCharacters.value - input.kweet.length;
        remainingKweetCharacters.value = 'Remaining ' + remainCharacters + ' characters';
      }
    }
  },
});
</script>

<template>
  <div class="flex flex-col">
    <h1 class="text-xl font-bold dark:text-gray-100 mb-7">Your feed</h1>
    <form class="w-full px-4 py-4 mb-3 bg-gray-800 border-b border-gray-600 rounded-lg" @submit.prevent="postKweet">
      <div class="mb-4">
        <textarea
          v-model="input.kweet"
          @keydown="remainingCharCount"
          rows="4"
          class="w-full p-4 mr-3 leading-tight text-gray-100 bg-gray-700 rounded-lg appearance-none focus:outline-none"
          type="text"
          placeholder="Whats up?"
        />
      </div>
      <div class="flex items-center justify-end space-x-4">
        <span class="h-full pr-4 text-xs border-r border-gray-600">{{ remainingKweetCharacters }}</span>
        <button class="flex-shrink-0 px-4 py-2 text-sm font-bold text-white rounded bg-red hover:bg-darkred" type="submit">Kweet</button>
      </div>
    </form>
    <!-- <div>{{ user }}</div> -->
  </div>
</template>
