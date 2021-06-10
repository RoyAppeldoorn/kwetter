import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { Kweet } from '../types';
import { KweetState } from './kweet.state';

export enum KweetGetterTypes {
  GET_HOME_FEED = 'GET_HOME_FEED',
}

export type KweetGetters = {
  [KweetGetterTypes.GET_HOME_FEED](state: KweetState): Kweet[] | null;
};

export const kweetGetters: GetterTree<KweetState, RootState> & KweetGetters = {
  [KweetGetterTypes.GET_HOME_FEED](state: KweetState): Kweet[] | null {
    return state.kweetHomeFeed;
  },
};
