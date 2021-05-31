import QueryResponse from '@/models/cqrs/queryResponse';
import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import { GetFollowByUserIdResponse } from '../dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from '../dto/response/getProfileDetailsResponse';
import { getProfileByName, getProfileFollowByUserId } from '../service';
import { ProfileMutations, ProfileMutationTypes } from './profile.mutations';
import { ProfileState } from './profile.state';

export enum ProfileActionTypes {
  GET_PROFILE_DETAILS = 'GET_PROFILE_DETAILS',
  GET_PROFILE_FOLLOWS = 'GET_PROFILE_FOLLOWS',
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
};
