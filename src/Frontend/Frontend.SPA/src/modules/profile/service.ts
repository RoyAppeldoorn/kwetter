import CommandResult from '@/models/cqrs/commandResult';
import QueryResponse from '@/models/cqrs/queryResponse';
import { httpClient } from '@/utils/httpClient';
import CreateFollowCommand from './dto/commands/createFollowCommand';
import GetFollowByUserIdQuery from './dto/queries/getFollowByUserIdQuery';
import GetProfileDetailsQuery from './dto/queries/getProfileDetailsQuery';
import { GetFollowByUserIdResponse } from './dto/response/getFollowByUserIdResponse';
import { GetProfileDetailsResponse } from './dto/response/getProfileDetailsResponse';
import DeleteFollowCommand from './dto/commands/deleteFollowCommand';

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
  const query: CreateFollowCommand = {
    followingId: followingId,
    followerId: followerId,
  };

  return await httpClient.post<CreateFollowCommand, CommandResult>('/api/follow', query);
};

export const deleteFollow = async (followingId: string, followerId: string): Promise<CommandResult> => {
  const query: DeleteFollowCommand = {
    followingId: followingId,
    followerId: followerId,
  };

  return await httpClient.delete<DeleteFollowCommand, CommandResult>('/api/follow', query);
};
