import { MutationTree } from 'vuex';
import { KweetState } from './kweet.state';

export enum KweetMutationTypes {}

export type KweetMutations = {};

export const kweetMutations: MutationTree<KweetState> & KweetMutations = {};
