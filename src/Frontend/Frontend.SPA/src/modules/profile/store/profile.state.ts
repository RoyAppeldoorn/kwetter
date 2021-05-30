import { Profile } from '../types';

export type ProfileState = {
  profile: Profile;
};

export const profileState: ProfileState = {
  profile: {
    id: 0,
    username: '',
    bio: '',
    location: '',
    pictureUrl: '',
  },
};
