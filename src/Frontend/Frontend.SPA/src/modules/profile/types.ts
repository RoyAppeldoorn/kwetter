export type Profile = {
  id: string;
  username: string;
  bio?: string;
  location?: string;
  pictureUrl?: string;
  followers?: Follower[];
  following?: Following[];
};

export type Follower = {
  id: string;
  username: string;
};

export type Following = {
  id: string;
  username: string;
};
