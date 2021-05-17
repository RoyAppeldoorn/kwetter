import AuthRoutes from '@/modules/auth/routes';
import UserRoutes from '@/modules/user/user.routes';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import NotFound from '../views/NotFound.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: Home,
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

// router.beforeEach(async (to, from, next) => {
//   if (to.matched.some((route) => route.meta.requiresAuth)) {
//     await requiresAuth({ to, from, next, store })
//   } else {
//     await isAuthenticated({ to, from, next, store })
//   }
// })

export default router;
