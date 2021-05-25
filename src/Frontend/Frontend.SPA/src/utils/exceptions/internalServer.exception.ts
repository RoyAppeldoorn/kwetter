import { StatusCodes } from 'http-status-codes';
import HttpException from './httpException';

class InternalServerException extends HttpException {
  constructor(message = 'Something went wrong...') {
    super(StatusCodes.INTERNAL_SERVER_ERROR, message);
  }
}

export default InternalServerException;
