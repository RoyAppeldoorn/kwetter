<script lang="ts">
import { reactive, toRefs, defineComponent, onBeforeMount, ref, onMounted, watch, computed } from 'vue';
import { useStore } from '@/store';
import { useRoute } from 'vue-router';
import { ProfileActionTypes } from '../store/profile.actions';
import { ProfileGetterTypes } from '../store/profile.getters';
import LoadingSpinner from '@/components/LoadingSpinner.vue';
import IconMapMarker from '@/icons/IconMapMarker.vue';
import { AuthActionTypes } from '@/modules/auth/store/auth.actions';
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';

export default defineComponent({
  name: 'Profile',
  components: {
    LoadingSpinner,
    IconMapMarker,
  },
  setup() {
    const state = reactive({
      count: 0,
      initialLoadDone: false,
    });

    const store = useStore();
    const route = useRoute();
    const handle = ref(route.params.name as string);
    const profile = computed(() => store.getters[ProfileGetterTypes.GET_PROFILE]);
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const followers = computed(() => store.getters[ProfileGetterTypes.GET_FOLLOWER_COUNT]);
    const following = computed(() => store.getters[ProfileGetterTypes.GET_FOLLOWING_COUNT]);

    onBeforeMount(async () => {
      await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, handle.value);
      state.initialLoadDone = true;
    });

    onMounted(async () => {
      await store.dispatch(ProfileActionTypes.GET_PROFILE_FOLLOWS, user.value!.userId);
      watch(
        () => route.params.name,
        async (name) => {
          if (name) {
            state.initialLoadDone = false;
            await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, name as string);
            await store.dispatch(ProfileActionTypes.GET_PROFILE_FOLLOWS, handle.value);
            state.initialLoadDone = true;
          }
        }
      );
    });

    return {
      ...toRefs(state),
      profile,
      followers,
      following,
    };
  },
});
</script>

<template>
  <div v-if="!initialLoadDone" class="flex flex-col">
    <div class="w-full text-center">
      <LoadingSpinner />
    </div>
  </div>
  <div v-else-if="!profile" class="flex flex-col">
    <div class="w-full text-center">Oops! Seems like the user doesn't exist!</div>
  </div>
  <div v-else>
    <div class="flex flex-col p-8 space-y-4 bg-gray-800 rounded-xl">
      <div class="block">
        <h1 class="text-2xl font-bold text-white">
          {{ profile.username }}
        </h1>
        <h2 class="text-gray-300">@{{ profile.username }}</h2>
      </div>
      <p class="font-light" v-show="profile.bio !== null">
        {{ profile.bio }}
      </p>
      <div class="flex flex-col flex-wrap w-full text-sm md:flex-row md:space-x-4" v-show="profile.bio !== null">
        <div v-show="profile.location !== null" class="flex items-center space-x-1 text-gray-300">
          <IconMapMarker />
          <p>{{ profile.location }}</p>
        </div>
        <div v-if="profile.followers">{{ followers }}</div>
        <div v-if="profile.followings">{{ following }}</div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
