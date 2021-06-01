<script lang="ts">
import { AuthActionTypes } from '@/modules/auth/store/auth.actions';
import { AuthGetterTypes } from '@/modules/auth/store/auth.getters';
import { auth } from '@/plugins/firebase';
import router from '@/router';
import { useStore } from '@/store';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  name: 'Header',
  setup() {
    const store = useStore();
    const loggedIn = computed(() => store.getters[AuthGetterTypes.GET_IS_LOGGED_IN]);

    const signOut = async () => {
      store.dispatch(AuthActionTypes.SET_USER_DATA, null);
      await auth.signOut();
      router.push('/login');
    };

    return { signOut, loggedIn };
  },
});
</script>

<template>
  <nav>
    <div class="px-2 mb-6 sm:px-6 lg:px-8">
      <div class="relative flex items-center justify-between h-16">
        <div class="flex items-center justify-between flex-1">
          <div class="flex items-center flex-shrink-0">
            <img class="w-auto" src="@/assets/logo.svg" alt="Kwetter" />
          </div>
          <div class="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 space-x-4 sm:pr-0">
            <button
              @click="signOut"
              class="px-2 py-1 text-gray-400 bg-gray-800 rounded-xl hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
            >
              Sign out
            </button>

            <button
              class="p-1 text-gray-400 bg-gray-800 rounded-xl hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
            >
              <span class="sr-only">View notifications</span>
              <!-- Heroicon name: outline/bell -->
              <svg class="w-6 h-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                />
              </svg>
            </button>

            <!-- Profile dropdown -->
            <div class="relative ml-3">
              <div>
                <button
                  type="button"
                  class="flex text-sm bg-gray-800 rounded-xl focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                  id="user-menu"
                  aria-expanded="false"
                  aria-haspopup="true"
                >
                  <span class="sr-only">Open user menu</span>
                  <img
                    class="w-8 h-8 rounded-xl"
                    src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
                    alt=""
                  />
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
