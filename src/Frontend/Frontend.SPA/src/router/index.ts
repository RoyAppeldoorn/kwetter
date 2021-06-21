import isAuthenticated from '@/guards/isAuthenticated';
import requiresAuth from '@/guards/requiresAuth';
import AuthRoutes from '@/modules/auth/routes';
import { rootStore } from '@/store';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import homeRoutes from '@/modules/home/routes';
import profileRoutes from '@/modules/profile/routes';
import catchAll from '@/modules/layout/routes';

const routes: RouteRecordRaw[] = [...AuthRoutes, homeRoutes, profileRoutes, catchAll];

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
