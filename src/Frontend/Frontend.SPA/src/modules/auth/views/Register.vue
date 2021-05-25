<script lang="ts">
import { useRoute, useRouter } from 'vue-router';
import { computed, defineComponent, reactive, ref } from 'vue';
import { toUserFromIdToken, User } from '../types';
import AuthService from '../service';
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

    const signOut = async () => {
      await auth.signOut();
    };

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
            const response: CommandResult = await AuthService.SetCustomClaims({
              idToken: idToken,
              userName: username.value,
            });
            if (response.success) {
              if (route.query && route.query.redirectTo) {
                router.push(route.query.redirectTo as string);
              } else {
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

    return { input, loading, inputEmpty, register, signOut };
  },
});
</script>

<template>
  <div class="container flex justify-center h-screen px-4 py-8 mx-auto text-center lg:px-0 dark">
    <div class="w-full sm:w-80">
      <img class="h-16 mx-auto sm:mt-12" src="@/assets/logo.svg" alt="Kwetter" />
      <h1 class="pt-10 text-4xl font-bold text-center dark:text-lightest">Register to Kwetter</h1>
      <form @submit.prevent="register">
        <input
          v-model="input.email"
          type="text"
          placeholder="Email"
          class="w-full px-2 py-4 my-6 text-xl transition-colors duration-75 border-2 rounded border-lighter dark:border-dark focus:outline-none dark:bg-black dark:text-light focus:border-blue dark:focus:border-blue"
        />
        <input
          v-model="input.username"
          type="text"
          placeholder="Username"
          class="w-full px-2 py-4 mb-6 text-xl transition-colors duration-75 border-2 rounded border-lighter dark:border-dark focus:outline-none dark:bg-black dark:text-light focus:border-blue dark:focus:border-blue"
        />
        <input
          v-model="input.password"
          type="password"
          placeholder="Password"
          class="w-full px-2 py-4 mb-6 text-xl transition-colors duration-75 border-2 rounded border-lighter dark:border-dark focus:outline-none dark:bg-black dark:text-light focus:border-blue dark:focus:border-blue"
        />
        <button
          type="submit"
          class="w-full h-auto p-4 mb-6 rounded-full bg-blue focus:outline-none"
          :class="inputEmpty ? 'cursor-not-allowed' : 'cursor-pointer hover:bg-darkblue'"
          :disabled="inputEmpty"
        >
          <span class="text-lg font-semibold text-lightest">Register</span>
        </button>
        <span v-if="loading">Loading...</span>
        <router-link to="/login">
          <span class="text-blue">Sign in for Kwetter</span>
        </router-link>
      </form>
      <button @click="signOut">Logout</button>
    </div>
  </div>
</template>
