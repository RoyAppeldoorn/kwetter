import CommandResult from '@/models/cqrs/commandResult';
import Guid from '@/utils/guid';
import { httpClient } from '@/utils/httpClient';
import CreateKweetCommand from './dto/CreateKweetCommand';

export const postKweet = async (command: CreateKweetCommand): Promise<CommandResult> => {
  if (!Guid.isValid(command.kweetId)) throw new Error('Invalid kweet id.');
  if (!Guid.isValid(command.userId)) throw new Error('Invalid user id.');
  if (command.message.length > 140) throw new Error('The message exceeded the maximum message length of 140.');

  return await httpClient.post<CreateKweetCommand, CommandResult>('/api/kweet', command);
};
