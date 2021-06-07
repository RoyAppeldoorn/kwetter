import CommandResult from '@/models/cqrs/commandResult';
import QueryResponse from '@/models/cqrs/queryResponse';
import { httpClient } from '@/utils/httpClient';
import CreateFollow from './dto/createFollow';
import GetFollowByUserIdQuery from './dto/getFollowByUserIdQuery';
import GetProfileDetailsQuery from './dto/getProfileDetailsQuery';
import { GetFollowByUserIdResponse } from './dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from './dto/response/getProfileDetailsResponse';
import UnFollow from './dto/deleteFollow';
import DeleteFollow from './dto/deleteFollow';

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

export const createFollow = async (followingId: string, followerId: string): Promise<CommandResult> => {
  const query: CreateFollow = {
    followingId: followingId,
    followerId: followerId,
  };

  return await httpClient.post<CreateFollow, CommandResult>('/api/follow', query);
};

export const deleteFollow = async (followingId: string, followerId: string): Promise<CommandResult> => {
  const query: DeleteFollow = {
    followingId: followingId,
    followerId: followerId,
  };

  return await httpClient.delete<DeleteFollow, CommandResult>('/api/follow', query);
};
