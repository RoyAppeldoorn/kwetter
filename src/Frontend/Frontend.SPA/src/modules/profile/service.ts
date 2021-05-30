import QueryResponse from '@/models/cqrs/queryResponse';
import { httpClient } from '@/utils/httpClient';
import GetProfileDetailsQuery from './dto/getProfileDetailsQuery';
import { Profile } from './types';

export const getProfileByName = async (username: string): Promise<QueryResponse<Profile>> => {
  const query: GetProfileDetailsQuery = {
    username: username,
  };

  return await httpClient.get<GetProfileDetailsQuery, QueryResponse<Profile>>(`/api/user/${query.username}`);
};
