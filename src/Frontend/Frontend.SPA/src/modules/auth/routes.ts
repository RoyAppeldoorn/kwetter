import { RouteRecordRaw } from 'vue-router';
import Register from './views/Register.vue';

const AuthRoutes: RouteRecordRaw[] = [
  {
    path: '/register',
    name: 'auth.register',
    component: Register,
  },
];

export default AuthRoutes;
