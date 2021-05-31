<script lang="ts">
import { defineComponent, onBeforeMount, ref, computed } from 'vue';
import { useStore } from '@/store';
import { useRoute } from 'vue-router';
import { ProfileActionTypes } from '../store/profile.actions';
import { ProfileGetterTypes } from '../store/profile.getters';
import LoadingSpinner from '@/components/LoadingSpinner.vue';
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import Return from '@/components/Return.vue';

export default defineComponent({
  name: 'Followers',
  components: {
    LoadingSpinner,
    Return,
  },
  setup() {
    const initialLoadDone = ref(false);
    const store = useStore();
    const route = useRoute();
    const handle = ref(route.params.name as string);
    const profile = computed(() => store.getters[ProfileGetterTypes.GET_PROFILE]);
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    const following = computed(() => store.getters[ProfileGetterTypes.GET_FOLLOWING]);

    onBeforeMount(async () => {
      if (profile.value == null) {
        await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, handle.value);
        await store.dispatch(ProfileActionTypes.GET_PROFILE_FOLLOWS, user.value!.userId);
      }

      initialLoadDone.value = true;
    });

    return {
      profile,
      following,
      initialLoadDone,
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
      <Return />
      {{ profile.following }}
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
