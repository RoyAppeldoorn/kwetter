import { MutationTree } from 'vuex';
import { GetFollowByUserIdResponse } from '../dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from '../dto/response/getProfileDetailsResponse';
import { Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileMutationTypes {
  SET_PROFILE_DATA = 'SET_PROFILE_DATA',
  SET_PROFILE_FOLLOWS = 'SET_PROFILE_FOLLOWS',
}

export type ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: GetProfileDetailsResponse): void;
  [ProfileMutationTypes.SET_PROFILE_FOLLOWS](state: ProfileState, follow: GetFollowByUserIdResponse): void;
};

export const profileMutations: MutationTree<ProfileState> & ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: GetProfileDetailsResponse): void {
    state.profile = profile;
  },
  [ProfileMutationTypes.SET_PROFILE_FOLLOWS](state: ProfileState, follow: GetFollowByUserIdResponse): void {
    if (state.profile) {
      state.profile.following = follow.following;
      state.profile.followers = follow.followers;
      console.log(follow.followers);
      console.log(follow.following);
    }
  },
};
