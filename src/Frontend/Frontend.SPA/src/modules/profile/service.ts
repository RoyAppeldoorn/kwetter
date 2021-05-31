import QueryResponse from '@/models/cqrs/queryResponse';
import { httpClient } from '@/utils/httpClient';
import GetFollowByUserIdQuery from './dto/getFollowByUserIdQuery';
import GetProfileDetailsQuery from './dto/getProfileDetailsQuery';
import { GetFollowByUserIdResponse } from './dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from './dto/response/getProfileDetailsResponse';

export const getProfileByName = async (username: string): Promise<QueryResponse<GetProfileDetailsResponse>> => {
  const query: GetProfileDetailsQuery = {
    username: username,
  };

  return await httpClient.get<GetProfileDetailsQuery, QueryResponse<GetProfileDetailsResponse>>(
    `/api/user/${query.username}`
  );
};

export const getProfileFollowByUserId = async (userId: string): Promise<QueryResponse<GetFollowByUserIdResponse>> => {
  const query: GetFollowByUserIdQuery = {
    id: userId,
  };

  return await httpClient.get<GetFollowByUserIdQuery, QueryResponse<GetFollowByUserIdResponse>>(
    `/api/follow/${query.id}`
  );
};
