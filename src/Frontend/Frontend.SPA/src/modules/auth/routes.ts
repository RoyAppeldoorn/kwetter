import { RouteRecordRaw } from 'vue-router';
import Register from './views/Register.vue';
import Login from './views/Login.vue';

const AuthRoutes: RouteRecordRaw[] = [
  {
    path: '/register',
    name: 'auth.register',
    component: Register,
  },
  {
    path: '/login',
    name: 'auth.login',
    component: Login,
  },
];

export default AuthRoutes;
