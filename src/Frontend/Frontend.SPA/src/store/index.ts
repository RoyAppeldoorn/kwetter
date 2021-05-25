import { AuthStore, authStore } from '@/modules/auth/store';
import { AuthState } from '@/modules/auth/store/auth.state';
import { KweetStore, kweetStore } from '@/modules/kweet/store';
import { KweetState } from '@/modules/kweet/store/kweet.state';
import { createLogger, createStore } from 'vuex';

export interface RootState {
  auth: AuthState;
  kweet: KweetState
}

export type RootStore = AuthStore<Pick<RootState, 'auth'>> & KweetStore<Pick<RootState, 'kweet'>>;

export const rootStore = createStore({
  modules: {
    auth: authStore,
    kweet: kweetStore
  },
  plugins: [createLogger()],
});

export function useStore() {
  return rootStore as RootStore;
}
