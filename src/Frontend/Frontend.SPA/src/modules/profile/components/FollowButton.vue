<template>
  <div>
    <button
      v-show="!isFollowing && !isCurrentUser"
      @click="followUserOrUnfollowUser"
      class="w-24 h-10 px-2 font-bold text-white border-2 rounded-xl group border-red bg-red hover:bg-darkred hover:border-darkred focus:outline-none"
    >
      Follow
    </button>
    <button
      v-show="!isCurrentUser && isFollowing"
      @click="followUserOrUnfollowUser"
      class="w-24 h-10 px-2 font-bold text-white border-2 rounded-xl group border-red bg-red hover:bg-darkred hover:border-darkred focus:outline-none"
    >
      <span class="group-hover:hidden">Following</span>
      <span class="hidden group-hover:block">Unfollow</span>
    </button>
  </div>
</template>

<script lang="ts">
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { useStore } from '@/store';
import { computed, defineComponent } from 'vue';
import { ProfileActionTypes } from '../store/profile.actions';
import { ProfileGetterTypes } from '../store/profile.getters';

export default defineComponent({
  name: 'FollowButton',
  setup() {
    const store = useStore();

    const profile = computed(() => store.getters[ProfileGetterTypes.GET_PROFILE]);
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const isFollowing = computed(() => store.getters[ProfileGetterTypes.IS_FOLLOWING](user.value!.userId));
    const isCurrentUser = computed(() => user.value?.userId === store.getters[ProfileGetterTypes.GET_PROFILE]?.id);

    async function followUserOrUnfollowUser() {
      if (isFollowing.value) {
        await store.dispatch(ProfileActionTypes.UNFOLLOW_USER, { followingId: profile.value!.id, followerId: user.value!.userId });
        return;
      }
      await store.dispatch(ProfileActionTypes.FOLLOW_USER, { followingId: profile.value!.id, followerId: user.value!.userId, name: profile.value!.username });
      console.log('follow');
    }

    return { followUserOrUnfollowUser, isCurrentUser, isFollowing, profile };
  },
});
</script>

<style scoped></style>
