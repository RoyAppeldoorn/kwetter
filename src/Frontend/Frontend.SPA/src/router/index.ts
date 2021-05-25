// import isAuthenticated from '@/guards/isAuthenticated';
import isAuthenticated from '@/guards/isAuthenticated';
import requiresAuth from '@/guards/requiresAuth';
import AuthRoutes from '@/modules/auth/routes';
import { rootStore } from '@/store';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import NotFound from '../views/NotFound.vue';
import homeRoutes from '@/modules/home/routes';

const routes: RouteRecordRaw[] = [
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  ...AuthRoutes,
  homeRoutes,
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: routes,
});

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((route) => route.meta.requiresAuth)) {
    await requiresAuth({ to, from, next, rootStore });
  } else {
    await isAuthenticated({ to, from, next, rootStore });
  }
});

export default router;
