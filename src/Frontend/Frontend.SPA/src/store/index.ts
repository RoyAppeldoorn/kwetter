import { AuthStore, authStore } from '@/modules/auth/store';
import { AuthState } from '@/modules/auth/store/auth.state';
import { createLogger, createStore } from 'vuex';

export interface RootState {
  auth: AuthState;
}

export type RootStore = AuthStore<Pick<RootState, 'auth'>>;

export const rootStore = createStore({
  modules: {
    auth: authStore,
  },
  plugins: [createLogger()],
});

export function useStore() {
  return rootStore as RootStore;
}
