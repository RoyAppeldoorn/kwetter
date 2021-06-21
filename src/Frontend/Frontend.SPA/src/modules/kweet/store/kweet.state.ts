import { Kweet } from '../types';

export type KweetState = {
  kweetHomeFeed: Kweet[] | null;
};

export const kweetState: KweetState = {
  kweetHomeFeed: null,
};
