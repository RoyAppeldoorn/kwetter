<template>
  <div>
    <button @click.prevent="setUser()">Set user</button>
  </div>
</template>

<script lang="ts">
import { useStore } from '@/store';
import { defineComponent } from 'vue';
import { UserActionTypes } from '../store/user.actions';
import { User } from '../user.types';

export default defineComponent({
  setup() {
    const store = useStore();

    let user: User = {
      userId: 'kut',
      profile: {
        picture: 'kek',
        email: 'kek',
        name: 'kek',
      },
    };

    const setUser = () => {
      store.dispatch(UserActionTypes.SET_USER, user);
    };

    return {
      setUser,
    };
  },
});
</script>

<style scoped></style>
