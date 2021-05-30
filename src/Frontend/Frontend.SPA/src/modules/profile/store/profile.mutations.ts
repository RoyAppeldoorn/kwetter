import { MutationTree } from 'vuex';
import { Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileMutationTypes {
  SET_PROFILE_DATA = 'SET_PROFILE_DATA',
}

export type ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: Profile): void;
};

export const profileMutations: MutationTree<ProfileState> & ProfileMutations = {
  [ProfileMutationTypes.SET_PROFILE_DATA](state: ProfileState, profile: Profile): void {
    state.profile = profile;
  },
};
