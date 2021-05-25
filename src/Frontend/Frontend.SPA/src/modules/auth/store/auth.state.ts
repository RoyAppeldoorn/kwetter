import { User } from '../types';

export type AuthState = {
  user: User | null;
};

export const authState: AuthState = {
  user: null,
};
