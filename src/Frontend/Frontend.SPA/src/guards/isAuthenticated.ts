import { AuthActionTypes } from '@/modules/auth/store/auth.actions';
import { toUserFromIdToken, User } from '@/modules/auth/types';
import { auth } from '@/plugins/firebase';
import { GuardContext } from '../types';

export default async function (ctx: GuardContext): Promise<void> {
  if (auth.currentUser) {
    const idToken = await auth.currentUser.getIdToken();
    const user: User = toUserFromIdToken(idToken);

    if (user.userId) {
      await ctx.rootStore.dispatch(AuthActionTypes.SET_USER_DATA, user);
    }

    if (ctx.to.path === '/login' || ctx.to.path === '/register') {
      ctx.next('/home');
    }
  } else {
    ctx.next();
  }
}
