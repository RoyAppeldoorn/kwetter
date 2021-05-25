<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
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
    const selectedTab = ref('home');
    const store = useStore();
    const showDropdown = ref(false);
    const showCreateFormDialog = ref(false);
    const user = computed(() => store.getters[AuthGetterTypes.GET_USER]);
    function selectTab(tab: string) {
      selectedTab.value = tab;
    }
    return {
      selectedTab,
      selectTab,
      showDropdown,
      showCreateFormDialog,
      user,
    };
  },
});
</script>

<template>
  <div class="lg:w-1/4 flex flex-col justify-between">
    <div>
      <NavigationSidebarTab id="home" label="Home" to="/home" :selected="selectedTab === 'home'" @selectTab="selectTab">
        <template #icon>
          <IconHome :size="28" class="mr-4 ml-1" />
        </template>
      </NavigationSidebarTab>
      <NavigationSidebarTab
        id="mentions"
        label="Mentions"
        to="/mentions"
        :selected="selectedTab === 'mentions'"
        @selectTab="selectTab"
      >
        <template #icon>
          <IconHashtag :size="28" class="mr-4 ml-1" />
        </template>
      </NavigationSidebarTab>
      <NavigationSidebarTab
        id="profile"
        label="Profile"
        to="/profile"
        :selected="selectedTab === 'profile'"
        @selectTab="selectTab"
      >
        <template #icon>
          <IconUser :size="28" class="mr-4 ml-1" />
        </template>
      </NavigationSidebarTab>
      <button
        class="text-gray-100 bg-red rounded-full font-semibold focus:outline-none w-12 h-12 lg:w-full lg:h-auto p-3 hover:bg-darkred transition-colors duration-75"
        @click="showCreateFormDialog = true"
      >
        <p class="hidden lg:block">Tweet</p>
        <IconPlus :size="24" class="inline-flex items-center justify-center lg:hidden" />
      </button>
    </div>
  </div>
</template>
