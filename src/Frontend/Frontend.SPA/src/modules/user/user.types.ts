export type RegistrationInput = {
  email: string;
  password: string;
};

export type User = {
  userId: string;
  profile: Profile;
};

export type Profile = {
  email: string;
  picture: string;
  name: string;
};
