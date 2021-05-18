// import isAuthenticated from '@/guards/isAuthenticated';
import AuthRoutes from '@/modules/auth/routes';
// import { rootStore } from '@/store'
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import NotFound from '../views/NotFound.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/home',
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
//     await isAuthenticated({ to, from, next, rootStore })
//   }
// })

export default router;
