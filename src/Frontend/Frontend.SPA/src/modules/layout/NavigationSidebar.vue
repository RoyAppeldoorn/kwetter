<script lang="ts">
import { computed, defineComponent } from 'vue';
import IconPlus from '../../icons/IconPlus.vue';
import NavigationSidebarTab from './NavigationSidebarTab.vue';
import IconHome from '../../icons/IconHome.vue';
import IconHashtag from '../../icons/IconHashtag.vue';
import IconUser from '../../icons/IconUser.vue';
import { useStore } from '@/store';
import { AuthGetterTypes } from '../auth/store/auth.getters';
export default defineComponent({
  name: 'ProfileSidebar',
  components: {
    IconPlus,
    NavigationSidebarTab,
    IconHome,
    IconHashtag,
    IconUser,
  },
  setup() {
    const store = useStore();
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    return {
      user,
    };
  },
});
</script>

<template>
  <div class="flex flex-col lg:w-1/3">
    <NavigationSidebarTab id="home" label="Home" to="/home">
      <template #icon>
        <IconHome :size="28" class="ml-1 mr-4" />
      </template>
    </NavigationSidebarTab>
    <NavigationSidebarTab id="mentions" label="Mentions" to="/mentions">
      <template #icon>
        <IconHashtag :size="28" class="ml-1 mr-4" />
      </template>
    </NavigationSidebarTab>
    <NavigationSidebarTab id="profile" label="Profile" :to="'/u/' + user.userName">
      <template #icon>
        <IconUser :size="28" class="ml-1 mr-4" />
      </template>
    </NavigationSidebarTab>
    <button
      class="w-12 h-12 p-3 font-semibold text-gray-100 transition-colors duration-75 rounded-full bg-red focus:outline-none lg:w-full lg:h-auto hover:bg-darkred"
      to="/home"
    >
      <p class="hidden lg:block">Kweet</p>
      <IconPlus :size="24" class="inline-flex items-center justify-center lg:hidden" />
    </button>
  </div>
</template>

<style scoped>
.router-link-active {
  @apply text-red;
}
.router-link-active > svg {
  @apply fill-current;
  @apply text-red;
}
</style>
