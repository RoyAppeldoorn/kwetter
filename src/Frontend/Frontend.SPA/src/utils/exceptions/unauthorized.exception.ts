import { StatusCodes } from 'http-status-codes';
import HttpException from './httpException';

class UnauthorizedException extends HttpException {
  constructor(message: string = 'Unauthorized...') {
    super(StatusCodes.UNAUTHORIZED, message);
  }
}

export default UnauthorizedException;
