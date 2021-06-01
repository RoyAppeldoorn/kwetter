import CommandResult from '@/models/cqrs/commandResult';
import QueryResponse from '@/models/cqrs/queryResponse';
import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import { GetFollowByUserIdResponse } from '../dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from '../dto/response/getProfileDetailsResponse';
import { createFollow, deleteFollow, getProfileByName, getProfileFollowByUserId } from '../service';
import { Follower } from '../types';
import { ProfileMutations, ProfileMutationTypes } from './profile.mutations';
import { ProfileState } from './profile.state';

export enum ProfileActionTypes {
  GET_PROFILE_DETAILS = 'GET_PROFILE_DETAILS',
  GET_PROFILE_FOLLOWS = 'GET_PROFILE_FOLLOWS',
  FOLLOW_USER = 'FOLLOW_USER',
  UNFOLLOW_USER = 'UFOLLOW_USER',
}

type AugmentedProfileActionContext = {
  commit<K extends keyof ProfileMutations>(
    key: K,
    payload: Parameters<ProfileMutations[K]>[1]
  ): ReturnType<ProfileMutations[K]>;
} & Omit<ActionContext<ProfileState, RootState>, 'commit'>;

export interface ProfileActions {
  [ProfileActionTypes.GET_PROFILE_DETAILS]({ commit }: AugmentedProfileActionContext, handle: string): void;
  [ProfileActionTypes.GET_PROFILE_FOLLOWS]({ commit }: AugmentedProfileActionContext, userId: string): void;
  [ProfileActionTypes.FOLLOW_USER]({ commit }: AugmentedProfileActionContext, payload: { followingId: string, followerId: string, name: string }): void;
  [ProfileActionTypes.UNFOLLOW_USER]({ commit }: AugmentedProfileActionContext, payload: { followingId: string, followerId: string }): void;
}

export const profileActions: ActionTree<ProfileState, RootState> & ProfileActions = {
  async [ProfileActionTypes.GET_PROFILE_DETAILS]({ commit }, handle) {
    await getProfileByName(handle)
      .then((res: QueryResponse<GetProfileDetailsResponse>) => {
        commit(ProfileMutationTypes.SET_PROFILE_DATA, res.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [ProfileActionTypes.GET_PROFILE_FOLLOWS]({ commit }, userId) {
    await getProfileFollowByUserId(userId)
      .then((res: QueryResponse<GetFollowByUserIdResponse>) => {
        commit(ProfileMutationTypes.SET_PROFILE_FOLLOWS, res.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [ProfileActionTypes.FOLLOW_USER]({ commit }: AugmentedProfileActionContext, payload: { followingId: string, followerId: string, name: string }) {
    await createFollow(payload.followingId, payload.followerId)
      .then((res: CommandResult) => {
        commit(ProfileMutationTypes.SET_IS_FOLLOWING, res.success);

        const follower: Follower = {
          id: payload.followerId,
          username: payload.name
        };

        commit(ProfileMutationTypes.ADD_FOLLOWER, follower);
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [ProfileActionTypes.UNFOLLOW_USER]({ commit }: AugmentedProfileActionContext, payload: { followingId: string, followerId: string }) {
    await deleteFollow(payload.followingId, payload.followerId)
      .then((res: CommandResult) => {
        commit(ProfileMutationTypes.REMOVE_FOLLOWER, payload.followerId);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
