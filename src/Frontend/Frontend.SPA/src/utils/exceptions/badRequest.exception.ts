import { StatusCodes } from 'http-status-codes';
import HttpException from './httpException';

class BadRequestException extends HttpException {
  constructor(message: string = 'Bad request...') {
    super(StatusCodes.BAD_REQUEST, message);
  }
}

export default BadRequestException;
