<script lang="ts">
import { useStore } from '@/store';
import { useRoute, useRouter } from 'vue-router';
import { computed, defineComponent, reactive, ref } from 'vue';
import { AuthActionTypes } from '../store/auth.actions';
import firebase from 'firebase';
import { toUserFromIdToken, User } from '../types';
import AuthService from '../service';

export default defineComponent({
  name: 'Register',
  setup() {
    const store = useStore();
    const route = useRoute();
    const router = useRouter();
    const email = ref('');
    const username = ref('');
    const password = ref('');
    const loading = ref(false);
    const input = reactive({ email, username, password });
    const inputEmpty = computed(() => input.email === '' || input.password === '' || input.username === '');

    async function register(): Promise<void> {
      loading.value = true;
      await firebase
        .auth()
        .createUserWithEmailAndPassword(email.value, password.value)
        .then(
          async (result) => {
            // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
            let idToken = await result.user!.getIdToken(true);
            let user: User = toUserFromIdToken(idToken);
            if (user.userId == null) {
              const response = await AuthService.SetCustomClaims({
                idToken: idToken,
                userName: username.value,
              });
              if (response.success) {
                // get new token with claims set!
                // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
                idToken = await result.user!.getIdToken(true);
                user = toUserFromIdToken(idToken);
                await store.dispatch(AuthActionTypes.SET_USER_DATA, user);
              }
            } else {
              await firebase.auth().signOut();
            }
          },
          (error) => {
            console.log(error);
          }
        );

      loading.value = false;
      if (store.getters.GET_USER) {
        if (route.query && route.query.redirectTo) {
          router.push(route.query.redirectTo as string);
        } else {
          router.push('/home');
        }
      }
    }

    return { input, loading, inputEmpty, register };
  },
});
</script>

<template>
  <div class="container flex justify-center w-1/4 h-screen px-4 py-8 mx-auto lg:px-0 dark">
    <div>
      <!-- <IconTwitter :size="60" class="w-12 h-12 text-blue" /> -->
      <h1 class="pt-12 text-4xl font-bold dark:text-lightest">Register to Kwetter</h1>
      <form @submit.prevent="register" class="w-full text-center">
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
          class="w-full h-auto p-4 rounded-full bg-blue focus:outline-none"
          :class="inputEmpty ? 'cursor-not-allowed' : 'cursor-pointer hover:bg-darkblue'"
          :disabled="inputEmpty"
        >
          <span class="text-lg font-semibold text-lightest">Register</span>
        </button>
        <router-link to="/login">
          <span class="text-blue">Sign in for Kwetter</span>
        </router-link>
      </form>
    </div>
  </div>
</template>
