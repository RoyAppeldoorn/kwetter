import { AuthStore, authStore } from '@/modules/auth/store';
import { AuthState } from '@/modules/auth/store/auth.state';
import { UserStore } from '@/modules/user/store';
import { IUserState } from '@/modules/user/store/User.state';
import { createLogger, createStore } from 'vuex';

export interface RootState {
  auth: AuthState;
  user: IUserState;
}

export type RootStore = UserStore<Pick<RootState, 'user'>> & AuthStore<Pick<RootState, 'auth'>>;

export const rootStore = createStore({
  modules: {
    auth: authStore,
  },
  plugins: [createLogger()],
});

export function useStore() {
  return rootStore as RootStore;
}
