import { MutationTree } from 'vuex';
import { User } from '../types';
import { AuthState } from './auth.state';

export enum AuthMutationTypes {
  SET_USER_DATA = 'SET_USER_DATA',
}

export type AuthMutations = {
  [AuthMutationTypes.SET_USER_DATA](state: AuthState, user: User | null): void;
};

export const authMutations: MutationTree<AuthState> & AuthMutations = {
  [AuthMutationTypes.SET_USER_DATA](state: AuthState, user: User | null): void {
    state.user = user;
  },
};
