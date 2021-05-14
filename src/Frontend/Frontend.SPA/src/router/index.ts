import UserRoutes from '@/modules/user/user.routes';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import NotFound from '../views/NotFound.vue';

const DefaultRoutes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: [...DefaultRoutes, ...UserRoutes],
});

export default router;
