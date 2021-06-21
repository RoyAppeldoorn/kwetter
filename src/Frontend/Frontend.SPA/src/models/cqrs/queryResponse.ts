import Response from './response';

export default interface QueryResponse<T> extends Response {
  data: T;
}
