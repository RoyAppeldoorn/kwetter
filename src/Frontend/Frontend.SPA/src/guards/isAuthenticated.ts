import { User } from '@/modules/auth/types';
import { AuthActionTypes } from '@/modules/auth/store/auth.actions';
import { UserGetterTypes } from '@/modules/user/store/user.getters';
import { GuardContext } from '../types';

export default async function (ctx: GuardContext) {
  if (ctx.store.getters[UserGetterTypes.GET_IS_LOGGED_IN]) ctx.next('/home');
  else ctx.next();
}
