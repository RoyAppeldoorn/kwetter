import { AuthActionTypes } from '@/modules/auth/store/auth.actions';
import { toUserFromIdToken, User } from '@/modules/auth/types';
import { auth } from '@/plugins/firebase';
import { GuardContext } from '../types';

export default async function (ctx: GuardContext): Promise<void> {
  if (auth.currentUser) {
    const idToken = await auth.currentUser.getIdToken();
    console.log(idToken);
    const user: User = toUserFromIdToken(idToken);

    if (user.userId) {
      ctx.rootStore.dispatch(AuthActionTypes.SET_USER_DATA, user);
    }

    ctx.next();
  } else {
    ctx.next({ path: '/login', query: { redirect: ctx.to.fullPath } });
  }
}
