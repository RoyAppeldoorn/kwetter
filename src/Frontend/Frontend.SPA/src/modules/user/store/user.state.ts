import { User } from '../user.types';

export interface IUserState {
  user: User | null;
}

export const userState: IUserState = {
  user: null,
};
