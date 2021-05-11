import { ActionTree, ActionContext } from 'vuex';
import { IUserState } from './User.state';
import { RootState } from '@/store';
import { UserMutations, UserMutationTypes } from './user.mutations';
import { User } from '../user.types';

export enum UserActionTypes {
  SET_USER = 'SET_USER',
}

type AugmentedUserActionContext = {
  commit<K extends keyof UserMutations>(key: K, payload: Parameters<UserMutations[K]>[1]): ReturnType<UserMutations[K]>;
} & Omit<ActionContext<IUserState, RootState>, 'commit'>;

export interface UserActions {
  [UserActionTypes.SET_USER]({ commit }: AugmentedUserActionContext, payload: User): void;
}

export const userActions: ActionTree<IUserState, RootState> & UserActions = {
  [UserActionTypes.SET_USER]({ commit }: AugmentedUserActionContext, payload: User): void {
    commit(UserMutationTypes.SET_USER, payload);
  },
};
