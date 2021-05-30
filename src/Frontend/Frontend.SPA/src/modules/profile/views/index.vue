<script lang="ts">
import { reactive, toRefs, defineComponent, onBeforeMount, ref, onMounted, watch, computed } from 'vue';
import { useStore } from '@/store';
import { useRoute } from 'vue-router';
import { ProfileActionTypes } from '../store/profile.actions';
import { ProfileGetterTypes } from '../store/profile.getters';
import LoadingSpinner from '@/components/LoadingSpinner.vue';

export default defineComponent({
  name: 'Profile',
  components: {
    LoadingSpinner,
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

    onBeforeMount(async () => {
      console.log('here');
      await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, handle.value);
      state.initialLoadDone = true;
    });

    onMounted(() => {
      watch(
        () => route.params.name,
        async (name) => {
          if (name) {
            state.initialLoadDone = false;
            await store.dispatch(ProfileActionTypes.GET_PROFILE_DETAILS, name as string);
            state.initialLoadDone = true;
          }
        }
      );
    });

    return {
      ...toRefs(state),
      profile,
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
  <div v-else>
    <div class="flex flex-col w-full">
      <div class="block w-full">
        <h1 class="text-2xl font-bold dark:text-lightest">
          {{ profile.username }}
        </h1>
        <h2 class="dark:text-gray">@{{ profile.username }}</h2>
      </div>
      <p class="w-full dark:text-lightest">
        {{ profile.bio }}
      </p>
      <div class="flex flex-col flex-wrap w-full text-sm md:flex-row md:space-x-4">
        <div v-show="profile.location !== ''" class="flex items-center space-x-1 dark:text-gray">
          <p>{{ profile.location }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
