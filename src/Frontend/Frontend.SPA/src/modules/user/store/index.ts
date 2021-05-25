import { Store, CommitOptions, DispatchOptions, Module } from 'vuex';
import { UserActions, userActions } from './user.actions';
import { UserGetters, userGetters } from './user.getters';
import { UserMutations, userMutations } from './user.mutations';
import { IUserState, IUserState as State, userState } from './user.state';

export type UserModule = {
  namespaced?: boolean;
  state?: IUserState;
  mutations?: UserMutations;
  actions?: UserActions;
  getters?: UserGetters;
};

export type UserStore<S = IUserState> = Omit<Store<S>, 'getters' | 'commit' | 'dispatch'> & {
  commit<K extends keyof UserMutations, P extends Parameters<UserMutations[K]>[1]>(
    key: K,
    payload: P,
    options?: CommitOptions
  ): ReturnType<UserMutations[K]>;
} & {
  dispatch<K extends keyof UserActions>(
    key: K,
    payload: Parameters<UserActions[K]>[1],
    options?: DispatchOptions
  ): ReturnType<UserActions[K]>;
} & {
  getters: {
    [K in keyof UserGetters]: ReturnType<UserGetters[K]>;
  };
};

export const userStore: Module<IUserState, IUserState> & UserModule = {
  namespaced: false,
  state: userState,
  mutations: userMutations,
  actions: userActions,
  getters: userGetters,
};
