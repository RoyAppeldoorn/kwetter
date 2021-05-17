import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { User } from '../types';
import { AuthState } from './auth.state';

export enum AuthGetterTypes {
  GET_IS_LOGGED_IN = 'GET_IS_LOGGED_IN',
  GET_USER = 'GET_USER',
}

export type AuthGetters = {
  [AuthGetterTypes.GET_IS_LOGGED_IN](state: AuthState): boolean;
  [AuthGetterTypes.GET_USER](state: AuthState): User | null;
};

export const authGetters: GetterTree<AuthState, RootState> & AuthGetters = {
  [AuthGetterTypes.GET_IS_LOGGED_IN](state: AuthState): boolean {
    if (state.user) return true;
    return false;
  },
  [AuthGetterTypes.GET_USER](state: AuthState): User | null {
    return state.user;
  },
};
