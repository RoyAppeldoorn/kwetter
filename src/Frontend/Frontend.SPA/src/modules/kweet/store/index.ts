import { Store, CommitOptions, DispatchOptions, Module } from 'vuex';
import { RootState } from '@/store';
import { kweetState, KweetState } from './kweet.state';
import { kweetMutations, KweetMutations } from './kweet.mutations';
import { kweetActions, KweetActions } from './kweet.actions';
import { kweetGetters, KweetGetters } from './kweet.getters';

export type KweetModule = {
  namespaced?: boolean;
  state?: KweetState;
  mutations?: KweetMutations;
  actions?: KweetActions;
  getters?: KweetGetters;
};

export type KweetStore<S = KweetState> = Omit<Store<S>, 'getters' | 'commit' | 'dispatch'> & {
  commit<K extends keyof KweetMutations, P extends Parameters<KweetMutations[K]>[1]>(
    key: K,
    payload: P,
    options?: CommitOptions
  ): ReturnType<KweetMutations[K]>;
} & {
  dispatch<K extends keyof KweetActions>(
    key: K,
    payload: Parameters<KweetActions[K]>[1],
    options?: DispatchOptions
  ): ReturnType<KweetActions[K]>;
} & {
  getters: {
    [K in keyof KweetGetters]: ReturnType<KweetGetters[K]>;
  };
};

export const kweetStore: Module<KweetState, RootState> & KweetModule = {
  namespaced: false,
  state: kweetState,
  mutations: kweetMutations,
  actions: kweetActions,
  getters: kweetGetters,
};
