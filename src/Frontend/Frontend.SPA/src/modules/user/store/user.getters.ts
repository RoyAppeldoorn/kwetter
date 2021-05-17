import { GetterTree } from 'vuex';
import { IUserState } from './User.state';
import { User } from '../user.types';

export enum UserGetterTypes {
  GET_IS_LOGGED_IN = 'GET_IS_LOGGED_IN',
  GET_USER = 'GET_USER',
}

export type UserGetters = {
  [UserGetterTypes.GET_IS_LOGGED_IN](state: IUserState): boolean;
  [UserGetterTypes.GET_USER](state: IUserState): User | null;
};

export const userGetters: GetterTree<IUserState, IUserState> & UserGetters = {
  [UserGetterTypes.GET_IS_LOGGED_IN](state: IUserState): boolean {
    if (state.user) return true;
    return false;
  },
  [UserGetterTypes.GET_USER](state: IUserState): User | null {
    return state.user;
  },
};
