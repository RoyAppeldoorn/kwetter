// import isAuthenticated from '@/guards/isAuthenticated';
import isAuthenticated from '@/guards/isAuthenticated';
import requiresAuth from '@/guards/requiresAuth';
import AuthRoutes from '@/modules/auth/routes';
import { rootStore } from '@/store';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import NotFound from '../views/NotFound.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/home',
    name: 'Home',
    component: Home,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  ...AuthRoutes,
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
