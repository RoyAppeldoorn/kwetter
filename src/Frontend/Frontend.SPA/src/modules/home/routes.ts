import { RouteRecordRaw } from 'vue-router';
import Home from './index.vue';
import { makeRoutesWithLayout } from '../../utils/makeRoutesWithLayout';
import BaseLayout from '../layout/BaseLayout.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/home',
    component: Home,
    meta: {
      requiresAuth: true,
    },
  },
];

export default makeRoutesWithLayout('/home', BaseLayout, routes);
