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
];

export default makeRoutesWithLayout('/u/:name', BaseLayout, routes);
