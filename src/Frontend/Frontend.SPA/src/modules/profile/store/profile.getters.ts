import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileGetterTypes {
  GET_PROFILE = 'GET_PROFILE',
  GET_FOLLOWER_COUNT = 'GET_FOLLOWER_COUNT',
  GET_FOLLOWING_COUNT = 'GET_FOLLOWING_COUNT',
}

export type ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile | null;
  [ProfileGetterTypes.GET_FOLLOWER_COUNT](state: ProfileState): number | undefined;
  [ProfileGetterTypes.GET_FOLLOWING_COUNT](state: ProfileState): number | undefined;
};

export const profileGetters: GetterTree<ProfileState, RootState> & ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile | null {
    return state.profile;
  },
  [ProfileGetterTypes.GET_FOLLOWER_COUNT](state: ProfileState): number | undefined {
    return state.profile?.followers?.length;
  },
  [ProfileGetterTypes.GET_FOLLOWING_COUNT](state: ProfileState): number | undefined {
    return state.profile?.followings?.length;
  },
};
