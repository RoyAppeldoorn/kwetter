import CommandResult from '@/models/cqrs/commandResult';
import QueryResponse from '@/models/cqrs/queryResponse';
import Guid from '@/utils/guid';
import { httpClient } from '@/utils/httpClient';
import CreateKweetCommand from './dto/commands/createKweetCommand';
import LikeKweetCommand from './dto/commands/likeKweetCommand';
import UnlikeKweetCommand from './dto/commands/unlikeKweetCommand';
import GetHomeFeedQuery from './dto/queries/getHomeFeedQuery';
import { Kweet } from './types';

export const postKweet = async (command: CreateKweetCommand): Promise<CommandResult> => {
  if (!Guid.isValid(command.kweetId)) throw new Error('Invalid kweet id.');
  if (!Guid.isValid(command.userId)) throw new Error('Invalid user id.');
  if (command.message.length > 140) throw new Error('The message exceeded the maximum message length of 140.');

  return await httpClient.post<CreateKweetCommand, CommandResult>('/api/kweet', command);
};

export const likeKweet = async (command: LikeKweetCommand): Promise<CommandResult> => {
  if (!Guid.isValid(command.kweetId)) throw new Error('Invalid kweet id.');
  if (!Guid.isValid(command.userId)) throw new Error('Invalid user id.');

  return await httpClient.post<LikeKweetCommand, CommandResult>('/api/kweet/like', command);
};

export const unlikeKweet = async (command: UnlikeKweetCommand): Promise<CommandResult> => {
  if (!Guid.isValid(command.kweetId)) throw new Error('Invalid kweet id.');
  if (!Guid.isValid(command.userId)) throw new Error('Invalid user id.');

  return await httpClient.delete<UnlikeKweetCommand, CommandResult>('/api/kweet/like', command);
};

export const getHomeFeed = async (pageNumber: number, pageSize: number): Promise<QueryResponse<Kweet[]>> => {
  if (pageNumber < 0) throw new Error('The page number can not be negative.');
  if (pageSize < 5) throw new Error('The page size can not be smaller than 5.');

  return await httpClient.get<GetHomeFeedQuery, QueryResponse<Kweet[]>>(
    `/api/timeline?pageNumber=${pageNumber}&pageSize=${pageSize}`
  );
};
