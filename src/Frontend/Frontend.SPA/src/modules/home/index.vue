<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent, onBeforeMount, reactive, ref } from 'vue';
import { KweetActionTypes } from '../kweet/store/kweet.actions';
import CreateKweetCommand from '../kweet/dto/commands/createKweetCommand';
import { User } from '../auth/types';
import Guid from '@/utils/guid';
import LoadingSpinner from '@/components/LoadingSpinner.vue';
import KweetCard from '../kweet/components/KweetCard.vue';
import { KweetGetterTypes } from '../kweet/store/kweet.getters';

export default defineComponent({
  components: { LoadingSpinner, KweetCard },
  name: 'Home',
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const kweet = ref('');
    const kweetMaxCharacters = ref(140);
    const remainingKweetCharacters = ref('Remaining 140 characters');
    const input = reactive({ kweet });
    const inputEmpty = computed(() => input.kweet === '');
    const pageNumber = ref(0);
    const pageSize = ref(5);
    const kweets = computed(() => store.getters[KweetGetterTypes.GET_HOME_FEED]);
    const isLoading = ref(true);

    onBeforeMount(async () => {
      await fetchTweets();
      isLoading.value = false;
    });

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

    async function fetchTweets(): Promise<void> {
      await store.dispatch(KweetActionTypes.GET_HOME_FEED, { pageNumber: pageNumber.value, pageSize: pageSize.value });
    }

    function remainingCharCount() {
      if (input.kweet.length > kweetMaxCharacters.value) {
        input.kweet = input.kweet.substring(0, kweetMaxCharacters.value);
      } else {
        var remainCharacters = kweetMaxCharacters.value - input.kweet.length;
        remainingKweetCharacters.value = 'Remaining ' + remainCharacters + ' characters';
      }
    }

    return { user, inputEmpty, postKweet, input, remainingKweetCharacters, remainingCharCount, kweets, isLoading };
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
        <button
          class="flex-shrink-0 px-4 py-2 text-sm font-bold text-white rounded bg-red hover:bg-darkred"
          type="submit"
        >
          Kweet
        </button>
      </div>
    </form>
    <div v-if="isLoading" class="flex justify-center">
      <LoadingSpinner />
    </div>
    <div v-else-if="!kweets.length" class="flex justify-center mt-2">
      Start following users to populate your timeline!
    </div>
    <div v-else>
      <div v-for="kweet in kweets" :key="kweet.id">
        <KweetCard :kweet="kweet" />
      </div>
    </div>
    <!-- <div>{{ user }}</div> -->
  </div>
</template>
