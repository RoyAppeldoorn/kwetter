import QueryResponse from '@/models/cqrs/queryResponse';
import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import { getProfileByName } from '../service';
import { Profile } from '../types';
import { ProfileMutations, ProfileMutationTypes } from './profile.mutations';
import { ProfileState } from './profile.state';

export enum ProfileActionTypes {
  GET_PROFILE_DETAILS = 'GET_PROFILE_DETAILS',
}

type AugmentedProfileActionContext = {
  commit<K extends keyof ProfileMutations>(
    key: K,
    payload: Parameters<ProfileMutations[K]>[1]
  ): ReturnType<ProfileMutations[K]>;
} & Omit<ActionContext<ProfileState, RootState>, 'commit'>;

export interface ProfileActions {
  [ProfileActionTypes.GET_PROFILE_DETAILS]({ commit }: AugmentedProfileActionContext, handle: string): void;
}

export const profileActions: ActionTree<ProfileState, RootState> & ProfileActions = {
  async [ProfileActionTypes.GET_PROFILE_DETAILS]({ commit }, handle) {
    await getProfileByName(handle)
      .then((res: QueryResponse<Profile>) => {
        commit(ProfileMutationTypes.SET_PROFILE_DATA, res.data);
        console.log('good!');
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
