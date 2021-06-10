<script lang="ts">
import IconHeart from '@/icons/IconHeart.vue';
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent, PropType, ref } from 'vue';
import { KweetActionTypes } from '../store/kweet.actions';
import { Kweet } from '../types';

export default defineComponent({
  components: { IconHeart },
  props: {
    kweet: {
      type: Object as PropType<Kweet>,
      required: true,
    },
  },
  setup(props) {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const likeCount = ref(props.kweet.likeCount);
    const alreadyLiked = ref(props.kweet.liked);

    function likeOrUnlikeKweet() {
      //   store.dispatch(KweetActionTypes.LIKE_KWEET, { userId: user.value!.userId, kweetId: props.kweet.id });
      //   return;

      if (alreadyLiked.value) {
        store.dispatch(KweetActionTypes.UNLIKE_KWEET, { userId: user.value!.userId, kweetId: props.kweet.id });
        likeCount.value--;
      } else {
        store.dispatch(KweetActionTypes.LIKE_KWEET, { userId: user.value!.userId, kweetId: props.kweet.id });
        likeCount.value++;
      }

      alreadyLiked.value = !alreadyLiked.value;
    }

    return { likeOrUnlikeKweet, alreadyLiked, likeCount, user };
  },
});
</script>

<template>
  <div class="p-6 mb-4 bg-gray-800 border-b border-gray-600 rounded-xl">
    <div class="flex">
      <img class="w-12 h-12 rounded-full" :src="'https://eu.ui-avatars.com/api/?name=' + user.userName" alt="" />
      <div class="flex flex-col ml-6">
        <div class="flex flex-wrap items-center">
          <span class="text-lg font-bold">{{ kweet.username }}</span>
          <span class="ml-2 text-sm text-gray-500 sm:ml-4">@{{ kweet.username }}</span>
          <span class="self-stretch ml-2 text-sm font-bold text-gray-500">.</span>
          <span class="ml-2 text-sm text-gray-500">{{ new Date(kweet.createdDateTime).toLocaleString() }}</span>
        </div>
        <div>
          {{ kweet.message }}
        </div>
      </div>
    </div>
    <div class="flex justify-end mt-4">
      <button
        @click="likeOrUnlikeKweet"
        v-show="alreadyLiked"
        class="flex items-center px-4 py-2 bg-gray-700 rounded-lg group"
      >
        <IconHeart class="fill-current text-red group-hover:text-darkred" />
        <span class="ml-2 text-red group-hover:text-darkred">{{ likeCount }}</span>
      </button>
      <button
        @click="likeOrUnlikeKweet"
        v-show="!alreadyLiked"
        class="flex items-center px-4 py-2 bg-gray-700 rounded-lg group"
      >
        <IconHeart class="text-gray-500 fill-current group-hover:text-red" />
        <span class="ml-2 text-gray-500 group-hover:text-red">{{ likeCount }}</span>
      </button>
    </div>
  </div>
</template>
