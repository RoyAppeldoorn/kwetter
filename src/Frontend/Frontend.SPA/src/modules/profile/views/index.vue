<script lang="ts">
import { defineComponent, onBeforeMount, ref, computed, onMounted, watch, onUnmounted } from 'vue';
import { useStore } from '@/store';
import { useRoute } from 'vue-router';
import { ProfileActionTypes } from '../store/profile.actions';
import { ProfileGetterTypes } from '../store/profile.getters';
import LoadingSpinner from '@/components/LoadingSpinner.vue';
import IconMapMarker from '@/icons/IconMapMarker.vue';
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import FollowButton from '../components/FollowButton.vue';

export default defineComponent({
  name: 'Profile',
  components: {
    LoadingSpinner,
    IconMapMarker,
    FollowButton,
  },
  setup() {
    const initialLoadDone = ref(false);
    const store = useStore();
    const route = useRoute();
    const handle = ref(route.params.name as string);
    const profile = computed(() => store.getters[ProfileGetterTypes.GET_PROFILE]);
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const followers = computed(() => store.getters[ProfileGetterTypes.GET_FOLLOWER_COUNT]);
    const following = computed(() => store.getters[ProfileGetterTypes.GET_FOLLOWING_COUNT]);
    const isFollowing = computed(() => store.getters[ProfileGetterTypes.IS_FOLLOWING](user.value!.userId));

    const isCurrentUser = computed(() => store.getters[AuthGetterTypes.GET_USER]?.userId === store.getters[ProfileGetterTypes.GET_PROFILE]?.id);

    onBeforeMount(async () => {
      await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, handle.value);
      let id = store.getters[ProfileGetterTypes.GET_PROFILE]?.id;
      if (id) await store.dispatch(ProfileActionTypes.GET_PROFILE_FOLLOWS, id);

      initialLoadDone.value = true;
    });

    onMounted(() => {
      watch(
        () => route.params.name,
        async (name) => {
          if (name) {
            initialLoadDone.value = false;
            await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, handle.value);
            let id = store.getters[ProfileGetterTypes.GET_PROFILE]?.id;
            if (id) await store.dispatch(ProfileActionTypes.GET_PROFILE_FOLLOWS, id);
            initialLoadDone.value = true;
          }
        }
      );
    });
    0;
    return {
      profile,
      followers,
      following,
      isCurrentUser,
      initialLoadDone,
      isFollowing,
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
      <div class="flex justify-between">
        <div>
          <h1 class="text-2xl font-bold text-white">
            {{ profile.username }}
          </h1>
          <h2 class="text-gray-300">@{{ profile.username }}</h2>
        </div>
        <FollowButton />
      </div>
      <p class="font-light" v-show="profile.bio !== null">
        {{ profile.bio }}
      </p>
      <div class="flex flex-col flex-wrap w-full text-sm md:flex-row md:space-x-4" v-show="profile.bio !== null">
        <div v-show="profile.location !== null" class="flex items-center space-x-1 text-gray-300">
          <IconMapMarker />
          <p>{{ profile.location }}</p>
        </div>
      </div>
      <div class="flex flex-col flex-wrap w-full text-sm md:flex-row md:space-x-4" v-show="profile.followings !== null">
        <div v-if="profile.following">
          <router-link :to="'/u/' + profile.username + '/following'">
            {{ following == 1 ? following + ' Following' : following + ' Followings' }}
          </router-link>
        </div>
        <div v-if="profile.followers">
          <router-link :to="'/u/' + profile.username + '/followers'">
            {{ followers == 1 ? followers + ' Follower' : followers + ' Followers' }}
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
