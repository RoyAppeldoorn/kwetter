<template>
  <div>
    <form @submit.prevent="register">
      <h3>Sign Up</h3>

      <div class="form-group">
        <label>Email</label>
        <input type="email" class="form-control form-control-lg" v-model="user.email" />
      </div>

      <div class="form-group">
        <label>Password</label>
        <input type="password" class="form-control form-control-lg" v-model="user.password" />
      </div>

      <button type="submit" class="btn btn-dark btn-lg btn-block">Sign Up</button>
    </form>

    <!-- <div>
      <button @click.prevent="setUser()">Set user</button>
    </div> -->
  </div>
</template>

<script lang="ts">
import { useStore } from '@/store';
import firebase from 'firebase';
import { defineComponent, ref } from 'vue';
import { UserActionTypes } from '../store/user.actions';
import { User } from '../user.types';

export default defineComponent({
  // setup() {
  //   const store = useStore();

  //   let user: User = {
  //     userId: 'kut',
  //     profile: {
  //       picture: 'kek',
  //       email: 'kek',
  //       name: 'kek',
  //     },
  //   };

  //   const setUser = () => {
  //     store.dispatch(UserActionTypes.SET_USER, user);
  //   };

  //   return {
  //     setUser,
  //   };
  // },
  data() {
    let user = ref({
      email: '',
      password: '',
    });

    return {
      user,
    };
  },
  methods: {
    async register(): Promise<void> {
      await firebase
        .auth()
        .createUserWithEmailAndPassword(this.user.email, this.user.password)
        .then((result) => {
          var idToken = result.user?.getIdToken(true);
          console.log(idToken);
        });
    },
  },
});
</script>

<style scoped></style>
