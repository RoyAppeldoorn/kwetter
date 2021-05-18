import { UserGetterTypes } from '@/modules/user/store/user.getters';
import { GuardContext } from '../types';

export default async function (ctx: GuardContext) {
  if (!ctx.rootStore.getters[UserGetterTypes.GET_IS_LOGGED_IN]) ctx.next('/login');
  else ctx.next();
}
