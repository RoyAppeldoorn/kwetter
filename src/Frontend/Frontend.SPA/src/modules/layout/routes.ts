import { RouteRecordRaw } from 'vue-router';
import { makeRoutesWithLayout } from '../../utils/makeRoutesWithLayout';
import BaseLayout from '../layout/BaseLayout.vue';
import NotFound from '@/views/NotFound.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '',
    component: NotFound,
  },
];

export default makeRoutesWithLayout('/:catchAll(.*)', BaseLayout, routes);
