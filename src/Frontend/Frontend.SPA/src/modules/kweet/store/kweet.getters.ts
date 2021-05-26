import { RootState } from '@/store';
import { GetterTree } from 'vuex';
import { KweetState } from './kweet.state';

export enum KweetGetterTypes {}

export type KweetGetters = {};

export const kweetGetters: GetterTree<KweetState, RootState> & KweetGetters = {};
