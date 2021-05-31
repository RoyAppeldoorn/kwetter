import { Follower, Following } from '../../types';

export type GetFollowByUserIdResponse = {
  id: string;
  username: string;
  followers: Follower[];
  followings: Following[];
};
