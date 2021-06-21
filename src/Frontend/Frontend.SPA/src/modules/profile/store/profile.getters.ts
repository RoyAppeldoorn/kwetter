import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { Follower, Following, Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileGetterTypes {
  GET_PROFILE = 'GET_PROFILE',
  GET_FOLLOWER_COUNT = 'GET_FOLLOWER_COUNT',
  GET_FOLLOWERS = 'GET_FOLLOWERS',
  GET_FOLLOWING_COUNT = 'GET_FOLLOWING_COUNT',
  GET_FOLLOWING = 'GET_FOLLOWING',
  IS_FOLLOWING = 'IS_FOLLOWING',
}

export type ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile | null;
  [ProfileGetterTypes.GET_FOLLOWER_COUNT](state: ProfileState): number | undefined;
  [ProfileGetterTypes.GET_FOLLOWERS](state: ProfileState): Follower[] | undefined;
  [ProfileGetterTypes.GET_FOLLOWING_COUNT](state: ProfileState): number | undefined;
  [ProfileGetterTypes.GET_FOLLOWING](state: ProfileState): Following[] | undefined;
  [ProfileGetterTypes.IS_FOLLOWING](state: ProfileState): (id: string) => boolean | undefined;
};

export const profileGetters: GetterTree<ProfileState, RootState> & ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile | null {
    return state.profile;
  },
  [ProfileGetterTypes.GET_FOLLOWER_COUNT](state: ProfileState): number | undefined {
    return state.profile?.followers?.length;
  },
  [ProfileGetterTypes.GET_FOLLOWERS](state: ProfileState): Follower[] | undefined {
    return state.profile?.followers;
  },
  [ProfileGetterTypes.GET_FOLLOWING_COUNT](state: ProfileState): number | undefined {
    return state.profile?.following?.length;
  },
  [ProfileGetterTypes.GET_FOLLOWING](state: ProfileState): Following[] | undefined {
    return state.profile?.following;
  },
  [ProfileGetterTypes.IS_FOLLOWING]: (state: ProfileState) => (id: string): boolean | undefined => {
    return state.profile?.followers?.some((x) => x.id === id);
  },
};
