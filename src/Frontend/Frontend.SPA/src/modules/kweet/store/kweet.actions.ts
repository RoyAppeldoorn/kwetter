import { RootState } from '@/store';
import { ActionContext, ActionTree } from 'vuex';
import CreateKweetCommand from '../dto/CreateKweetCommand';
import { KweetMutations } from './kweet.mutations';
import { KweetState } from './kweet.state';
import { postKweet } from '../service';

export enum KweetActionTypes {
  POST_KWEET = 'POST_KWEET',
}

type AugmentedAuthActionContext = {
  commit<K extends keyof KweetMutations>(
    key: K,
    payload: Parameters<KweetMutations[K]>[1]
  ): ReturnType<KweetMutations[K]>;
} & Omit<ActionContext<KweetState, RootState>, 'commit'>;

export interface KweetActions {
  [KweetActionTypes.POST_KWEET]({ commit }: AugmentedAuthActionContext, payload: CreateKweetCommand): void;
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
};
