import { RouteRecordRaw } from 'vue-router';
import { makeRoutesWithLayout } from '../../utils/makeRoutesWithLayout';
import BaseLayout from '../layout/BaseLayout.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '',
    component: () => import('./views/index.vue'),
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: 'following',
    component: () => import('./views/following.vue'),
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: 'followers',
    component: () => import('./views/followers.vue'),
    meta: {
      requiresAuth: true,
    },
  },
];

export default makeRoutesWithLayout('/u/:name', BaseLayout, routes);
