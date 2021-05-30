import { Profile } from '../types';

export type ProfileState = {
  profile: Profile | null;
};

export const profileState: ProfileState = {
  profile: null,
};
