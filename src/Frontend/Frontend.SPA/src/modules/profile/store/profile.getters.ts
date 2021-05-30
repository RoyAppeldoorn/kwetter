import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { Profile } from '../types';
import { ProfileState } from './profile.state';

export enum ProfileGetterTypes {
  GET_PROFILE = 'GET_PROFILE',
}

export type ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile;
};

export const profileGetters: GetterTree<ProfileState, RootState> & ProfileGetters = {
  [ProfileGetterTypes.GET_PROFILE](state: ProfileState): Profile {
    return state.profile;
  },
};
