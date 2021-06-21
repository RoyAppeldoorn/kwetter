import { Store, CommitOptions, DispatchOptions, Module } from 'vuex';
import { RootState } from '@/store';
import { profileState, ProfileState } from './profile.state';
import { profileMutations, ProfileMutations } from './profile.mutations';
import { profileActions, ProfileActions } from './profile.actions';
import { profileGetters, ProfileGetters } from './profile.getters';

export type ProfileModule = {
  namespaced?: boolean;
  state?: ProfileState;
  mutations?: ProfileMutations;
  actions?: ProfileActions;
  getters?: ProfileGetters;
};

export type ProfileStore<S = ProfileState> = Omit<Store<S>, 'getters' | 'commit' | 'dispatch'> & {
  commit<K extends keyof ProfileMutations, P extends Parameters<ProfileMutations[K]>[1]>(
    key: K,
    payload: P,
    options?: CommitOptions
  ): ReturnType<ProfileMutations[K]>;
} & {
  dispatch<K extends keyof ProfileActions>(
    key: K,
    payload: Parameters<ProfileActions[K]>[1],
    options?: DispatchOptions
  ): ReturnType<ProfileActions[K]>;
} & {
  getters: {
    [K in keyof ProfileGetters]: ReturnType<ProfileGetters[K]>;
  };
};

export const profileStore: Module<ProfileState, RootState> & ProfileModule = {
  namespaced: false,
  state: profileState,
  mutations: profileMutations,
  actions: profileActions,
  getters: profileGetters,
};
