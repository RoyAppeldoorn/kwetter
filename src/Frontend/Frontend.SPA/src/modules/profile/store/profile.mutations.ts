import { MutationTree } from 'vuex';
import { GetFollowByUserIdResponse } from '../dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from '../dto/response/getProfileDetailsResponse';
import { Follower, Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileMutationTypes {
  SET_PROFILE_DATA = 'SET_PROFILE_DATA',
  SET_PROFILE_FOLLOWS = 'SET_PROFILE_FOLLOWS',
  SET_IS_FOLLOWING = 'SET_IS_FOLLOWING',
  ADD_FOLLOWER = 'ADD_FOLOWER',
  REMOVE_FOLLOWER = 'REMOVE_FOLLOWER',
}

export type ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: GetProfileDetailsResponse): void;
  [ProfileMutationTypes.SET_PROFILE_FOLLOWS](state: ProfileState, follow: GetFollowByUserIdResponse): void;
  [ProfileMutationTypes.SET_IS_FOLLOWING](state: ProfileState, payload: boolean): void;
  [ProfileMutationTypes.ADD_FOLLOWER](state: ProfileState, payload: Follower): void;
  [ProfileMutationTypes.REMOVE_FOLLOWER](state: ProfileState, id: string): void;
};

export const profileMutations: MutationTree<ProfileState> & ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: GetProfileDetailsResponse): void {
    state.profile = profile;
  },
  [ProfileMutationTypes.SET_PROFILE_FOLLOWS](state: ProfileState, follow: GetFollowByUserIdResponse): void {
    if (state.profile) {
      state.profile.following = follow.following;
      state.profile.followers = follow.followers;
    }
  },
  [ProfileMutationTypes.SET_IS_FOLLOWING](state: ProfileState, payload: boolean): void {
    if (state.profile) {
      state.profile.isFollowing = payload;
    }
  },
  [ProfileMutationTypes.ADD_FOLLOWER](state: ProfileState, payload: Follower): void {
    if (state.profile) {
      state.profile.followers?.push(payload);
    }
  },
  [ProfileMutationTypes.REMOVE_FOLLOWER](state: ProfileState, id: string): void {
    if (state.profile) {
      state.profile.followers! = state.profile.followers!.filter((e) => e.id !==id);
    }
  },
};
