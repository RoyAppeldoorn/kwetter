import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import CreateKweetCommand from '../dto/commands/createKweetCommand';
import { KweetMutations, KweetMutationTypes } from './kweet.mutations';
import { KweetState } from './kweet.state';
import { getHomeFeed, likeKweet, postKweet, unlikeKweet } from '../service';
import QueryResponse from '@/models/cqrs/queryResponse';
import { Kweet } from '../types';
import LikeKweetCommand from '../dto/commands/likeKweetCommand';
import UnlikeKweetCommand from '../dto/commands/unlikeKweetCommand';

export enum KweetActionTypes {
  POST_KWEET = 'POST_KWEET',
  LIKE_KWEET = 'LIKE_KWEET',
  UNLIKE_KWEET = 'UNLIKE_KWEET',
  GET_HOME_FEED = 'GET_HOME_FEED',
}

type AugmentedAuthActionContext = {
  commit<K extends keyof KweetMutations>(
    key: K,
    payload: Parameters<KweetMutations[K]>[1]
  ): ReturnType<KweetMutations[K]>;
} & Omit<ActionContext<KweetState, RootState>, 'commit'>;

export interface KweetActions {
  [KweetActionTypes.POST_KWEET]({ commit }: AugmentedAuthActionContext, payload: CreateKweetCommand): void;
  [KweetActionTypes.LIKE_KWEET]({ commit }: AugmentedAuthActionContext, payload: LikeKweetCommand): void;
  [KweetActionTypes.UNLIKE_KWEET]({ commit }: AugmentedAuthActionContext, payload: UnlikeKweetCommand): void;
  [KweetActionTypes.GET_HOME_FEED](
    { commit }: AugmentedAuthActionContext,
    payload: { pageNumber: number; pageSize: number }
  ): void;
}

export const kweetActions: ActionTree<KweetState, RootState> & KweetActions = {
  async [KweetActionTypes.POST_KWEET]({ commit }: AugmentedAuthActionContext, payload: CreateKweetCommand) {
    await postKweet(payload)
      .then(() => {
        console.log('good!');
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [KweetActionTypes.LIKE_KWEET]({ commit }: AugmentedAuthActionContext, payload: LikeKweetCommand) {
    await likeKweet(payload)
      .then(() => {
        commit(KweetMutationTypes.LIKE_KWEET, payload.kweetId);
        console.log('liked!');
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [KweetActionTypes.UNLIKE_KWEET]({ commit }: AugmentedAuthActionContext, payload: UnlikeKweetCommand) {
    await unlikeKweet(payload)
      .then(() => {
        commit(KweetMutationTypes.UNLIKE_KWEET, payload.kweetId);
        console.log('unliked!');
      })
      .catch((error) => {
        console.log(error);
      });
  },
  async [KweetActionTypes.GET_HOME_FEED](
    { commit }: AugmentedAuthActionContext,
    payload: { pageNumber: number; pageSize: number }
  ) {
    await getHomeFeed(payload.pageNumber, payload.pageSize)
      .then((res: QueryResponse<Kweet[]>) => {
        commit(KweetMutationTypes.SET_HOME_FEED, res.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
