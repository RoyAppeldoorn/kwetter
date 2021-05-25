import jwtDecode, { JwtPayload } from 'jwt-decode';

export type User = {
  userId: string;
  userName: string;
  email: string;
};

export type ClaimsRequest = {
  idToken: string;
};

export function toUserFromIdToken(idToken: string): User {
  const payload: GoogleJwtPayload = jwtDecode<GoogleJwtPayload>(idToken);
  const user: User = {
    userId: payload.UserId,
    userName: payload.UserName,
    email: payload.email,
  };
  return user;
}

interface GoogleJwtPayload extends JwtPayload {
  email: string;
  email_verified: boolean;
  azp: string;
  at_hash: string;
  locale: string;
  iat: number;
  exp: number;
  UserId: string;
  UserName: string;
  User: boolean;
}
