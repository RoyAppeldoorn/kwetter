import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { RootStore } from './store';

export type GuardContext = {
  to: RouteLocationNormalized;
  from: RouteLocationNormalized;
  next: NavigationGuardNext;
  store: RootStore;
};
