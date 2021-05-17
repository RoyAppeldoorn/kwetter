import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import { User } from '../types';
import { AuthMutations, AuthMutationTypes } from './auth.mutations';
import { AuthState } from './auth.state';

export enum AuthActionTypes {
  SET_USER_DATA = 'SET_USER_DATA',
}

type AugmentedAuthActionContext = {
  commit<K extends keyof AuthMutations>(key: K, payload: Parameters<AuthMutations[K]>[1]): ReturnType<AuthMutations[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

export interface AuthActions {
  [AuthActionTypes.SET_USER_DATA]({ commit }: AugmentedAuthActionContext, payload: User): void;
}

export const authActions: ActionTree<AuthState, RootState> & AuthActions = {
  [AuthActionTypes.SET_USER_DATA]({ commit }: AugmentedAuthActionContext, payload: User): void {
    console.log(payload);
    commit(AuthMutationTypes.SET_USER_DATA, payload);
  },
};
