<script lang="ts">
import { useRoute, useRouter } from 'vue-router';
import { computed, defineComponent, reactive, ref } from 'vue';
import { auth } from '@/plugins/firebase';

export default defineComponent({
  name: 'Register',
  setup() {
    const route = useRoute();
    const router = useRouter();
    const email = ref('');
    const password = ref('');
    const loading = ref(false);
    const input = reactive({ email, password });
    const inputEmpty = computed(() => input.email === '' || input.password === '');

    async function signIn(): Promise<void> {
      loading.value = true;
      await auth
        .signInWithEmailAndPassword(email.value, password.value)
        .then(() => {
          if (route.query && route.query.redirectTo) {
            router.push(route.query.redirectTo as string);
          } else {
            router.push('/home');
          }
        })
        .catch((err) => {
          console.log(err);
        })
        .finally(() => {
          loading.value = false;
        });
    }

    return { input, loading, inputEmpty, signIn };
  },
});
</script>

<template>
  <div class="container flex justify-center h-screen px-4 py-8 mx-auto lg:px-0 dark">
    <div class="w-full sm:w-80">
      <img class="h-16 mx-auto sm:mt-12" src="@/assets/logo.svg" alt="Kwetter" />
      <h1 class="pt-12 text-4xl font-bold text-center dark:text-lightest">Sign in to Kwetter</h1>
      <form @submit.prevent="signIn" class="text-center">
        <input
          v-model="input.email"
          type="text"
          placeholder="Email"
          class="w-full px-2 py-4 my-6 text-xl transition-colors duration-75 border-2 rounded border-lighter dark:border-dark focus:outline-none dark:bg-black dark:text-light focus:border-blue dark:focus:border-blue"
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
          <span class="text-lg font-semibold text-lightest">{{ loading ? 'Loading' : 'Sign in' }}</span>
        </button>
        <router-link to="/register">
          <span class="text-blue">Sign up for Kwetter</span>
        </router-link>
      </form>
    </div>
  </div>
</template>
