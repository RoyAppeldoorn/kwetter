import CommandResult from '@/models/cqrs/commandResult';
import api from '@/utils/axios';
import HttpRequestHandler from '@/utils/httpRequestHandler';
import CreateKweetCommand from './dto/CreateKweetCommand';

class KweetService {
  async CreateKweet(createKweetCommand: CreateKweetCommand) {
    return await api.post<CreateKweetCommand, CommandResult>('/api/kweet', createKweetCommand)
  }
}

export default new KweetService();
