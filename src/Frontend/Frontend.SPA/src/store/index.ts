import { IUserState, userStore, UserStore } from '@/modules/user/store';
import { createLogger, createStore } from 'vuex';

export type RootState = {
  user: IUserState;
};

export type RootStore = UserStore<Pick<RootState, 'user'>>;

export const rootStore = createStore({
  modules: {
    user: userStore,
  },
  plugins: [createLogger()],
});

export function useStore() {
  return rootStore as RootStore;
}
