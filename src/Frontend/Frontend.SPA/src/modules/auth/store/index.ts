import { Store, CommitOptions, DispatchOptions, Module } from 'vuex';
import { authMutations, AuthMutations } from './auth.mutations';
import { authActions, AuthActions } from './auth.actions';
import { authGetters, AuthGetters } from './auth.getters';
import { authState, AuthState } from './auth.state';
import { RootState } from '@/store';

export type AuthModule = {
  namespaced?: boolean;
  state?: AuthState;
  mutations?: AuthMutations;
  actions?: AuthActions;
  getters?: AuthGetters;
};

export type AuthStore<S = AuthState> = Omit<Store<S>, 'getters' | 'commit' | 'dispatch'> & {
  commit<K extends keyof AuthMutations, P extends Parameters<AuthMutations[K]>[1]>(
    key: K,
    payload: P,
    options?: CommitOptions
  ): ReturnType<AuthMutations[K]>;
} & {
  dispatch<K extends keyof AuthActions>(
    key: K,
    payload: Parameters<AuthActions[K]>[1],
    options?: DispatchOptions
  ): ReturnType<AuthActions[K]>;
} & {
  getters: {
    [K in keyof AuthGetters]: ReturnType<AuthGetters[K]>;
  };
};

export const authStore: Module<AuthState, RootState> & AuthModule = {
  namespaced: true,
  state: authState,
  mutations: authMutations,
  actions: authActions,
  getters: authGetters,
};
