import CommandResult from '@/models/cqrs/commandResult';
import { httpClient } from '@/utils/httpClient';
import ClaimsCommand from './dto/claimsCommand';

export const setCustomClaims = async (command: ClaimsCommand): Promise<CommandResult> => {
  return await httpClient.post<ClaimsCommand, CommandResult>('/api/authorization/claims', command);
};
