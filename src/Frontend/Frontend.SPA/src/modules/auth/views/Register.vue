<script lang="ts">
import { useRoute, useRouter } from 'vue-router';
import { computed, defineComponent, reactive, ref } from 'vue';
import { toUserFromIdToken, User } from '../types';
import { setCustomClaims } from '../service';
import CommandResult from '@/models/cqrs/commandResult';
import { auth } from '@/plugins/firebase';

export default defineComponent({
  name: 'Register',
  setup() {
    const route = useRoute();
    const router = useRouter();
    const email = ref('');
    const username = ref('');
    const password = ref('');
    const loading = ref(false);
    const error = ref('');
    const input = reactive({ email, username, password });
    const inputEmpty = computed(() => input.email === '' || input.password === '' || input.username === '');

    async function register(): Promise<void> {
      loading.value = true;
      await auth
        .createUserWithEmailAndPassword(email.value, password.value)
        .then(async (result) => {
          // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
          let idToken = await result.user!.getIdToken(true);
          let user: User = toUserFromIdToken(idToken);

          if (user.userId == null) {
            //TODO: Make sure firebase user gets custom claims
            const response: CommandResult = await setCustomClaims({
              idToken: idToken,
              userName: username.value,
            });
            if (response.success) {
              if (route.query && route.query.redirectTo) {
                router.push(route.query.redirectTo as string);
              } else {
                // refresh IdToken so it contains the newly added claims
                await result.user!.getIdToken(true);
                router.push('/home');
              }
            } else {
              loading.value = false;
            }
          }
        })
        .catch((err) => {
          error.value = err.code;
        })
        .finally(() => {
          loading.value = false;
        });
    }

    return { input, loading, inputEmpty, register };
  },
});
</script>

<template>
  <div class="container flex justify-center h-screen px-4 py-8 mx-auto text-center lg:px-0 dark">
    <div class="w-full sm:w-80">
      <img class="h-16 mx-auto sm:mt-12" src="@/assets/logo.svg" alt="Kwetter" />
      <h1 class="pt-10 text-4xl font-bold text-center">Register to Kwetter</h1>
      <form @submit.prevent="register">
        <input
          v-model="input.email"
          type="text"
          placeholder="Email"
          class="w-full px-2 py-4 my-6 text-xl border-2 rounded focus:outline-none dark:bg-black dark:text-gray-100 focus:border-red dark:focus:border-red"
        />
        <input
          v-model="input.username"
          type="text"
          placeholder="Username"
          class="w-full px-2 py-4 mb-6 text-xl border-2 rounded focus:outline-none dark:bg-black dark:text-gray-100 focus:border-red dark:focus:border-red"
        />
        <input
          v-model="input.password"
          type="password"
          placeholder="Password"
          class="w-full px-2 py-4 mb-6 text-xl border-2 rounded focus:outline-none dark:bg-black dark:text-gray-100 focus:border-red dark:focus:border-red"
        />
        <button
          type="submit"
          class="w-full h-auto p-4 mb-6 rounded-full bg-red focus:outline-none"
          :class="inputEmpty ? 'cursor-not-allowed' : 'cursor-pointer hover:bg-darkred'"
          :disabled="inputEmpty"
        >
          <span class="text-lg font-semibold text-gray-100">{{ loading ? 'Loading..' : 'Sign up' }}</span>
        </button>
        <router-link to="/login">
          <span class="text-red hover:text-darkred">Sign in for Kwetter</span>
        </router-link>
      </form>
    </div>
  </div>
</template>
