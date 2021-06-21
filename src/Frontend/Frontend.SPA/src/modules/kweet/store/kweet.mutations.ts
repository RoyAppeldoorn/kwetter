import { MutationTree } from 'vuex';
import { Kweet } from '../types';
import { KweetState } from './kweet.state';

export enum KweetMutationTypes {
  SET_HOME_FEED = 'SET_HOME_FEED',
  LIKE_KWEET = 'SET_KWEET_LIKE',
  UNLIKE_KWEET = 'DELETE_KWEET_LIKE',
}

export type KweetMutations = {
  [KweetMutationTypes.SET_HOME_FEED](state: KweetState, kweets: Kweet[]): void;
  [KweetMutationTypes.LIKE_KWEET](state: KweetState, kweetId: string): void;
  [KweetMutationTypes.UNLIKE_KWEET](state: KweetState, kweetId: string): void;
};

export const kweetMutations: MutationTree<KweetState> & KweetMutations = {
  [KweetMutationTypes.SET_HOME_FEED](state: KweetState, kweets: Kweet[]): void {
    state.kweetHomeFeed = kweets;
  },
  [KweetMutationTypes.LIKE_KWEET](state: KweetState, kweetId: string): void {
    const index = state.kweetHomeFeed!.findIndex((k) => k.id === kweetId);
    state.kweetHomeFeed![index].liked = true;
  },
  [KweetMutationTypes.UNLIKE_KWEET](state: KweetState, kweetId: string): void {
    const index = state.kweetHomeFeed!.findIndex((k) => k.id === kweetId);
    state.kweetHomeFeed![index].liked = false;
  },
};
