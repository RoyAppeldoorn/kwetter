import CommandResult from '@/models/cqrs/commandResult';
import api from '@/utils/axios';
import HttpRequestHandler from '@/utils/httpRequestHandler';
import { AxiosResponse } from 'axios';
import ClaimsCommand from './dto/ClaimsCommand';

class AuthService {
  async SetCustomClaims(claimCommand: ClaimsCommand) {
    return HttpRequestHandler.post<ClaimsCommand, CommandResult>('/api/authorization/claims', claimCommand);
  }
}

export default new AuthService();
