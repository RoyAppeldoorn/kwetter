import axios, { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
import { StatusCodes } from 'http-status-codes';
import BadRequestException from './exceptions/badRequest.exception';
import HttpException from './exceptions/httpException';
import InternalServerException from './exceptions/internalServer.exception';
import UnauthorizedException from './exceptions/unauthorized.exception';

/**
 * Represents the HttpCommunicator class.
 * Used to communicate with external HTTP endpoints.
 */
export default class HttpRequestHandler {
  private _gatewayUrl: string;
  private _api: AxiosInstance;

  constructor(gatewayUrl: string) {
    this._gatewayUrl = gatewayUrl;
    this._api = axios.create({
      baseURL: this._gatewayUrl,
    });
  }

  public async get<TResponse extends Response>(path: string, config?: AxiosRequestConfig): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.get(path, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public async post<TRequest, TResponse extends Response>(path: string, body: TRequest, config?: AxiosRequestConfig): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.post(path, body, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public async put<TRequest, TResponse extends Response>(path: string, body: TRequest, config?: AxiosRequestConfig): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.put(path, body, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public async delete<TRequest, TResponse extends Response>(path: string, body: TRequest, config?: AxiosRequestConfig): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.delete(path, body);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  //   private async getHeaders(): Promise<Headers> {
  //     const headers: Headers = new Headers();
  //     headers.append('Content-Type', 'application/json');
  //     const firebaseUser: firebase.User | null = this._firebaseApp.auth().currentUser;
  //     if (firebaseUser) headers.append('Authorization', `Bearer ${await firebaseUser.getIdToken()}`);
  //     return headers;
  //   }

  private checkStatusCode(err: AxiosError<any>): HttpException {
    if (!err || !err.response) return new InternalServerException();
    const message: string = err.response.data.message;
    switch (err.response.status) {
      case StatusCodes.UNAUTHORIZED:
        return new UnauthorizedException(message);
      case StatusCodes.BAD_REQUEST:
        return new BadRequestException(message);
      case StatusCodes.INTERNAL_SERVER_ERROR:
        return new InternalServerException(message);
      default:
        return new InternalServerException(message);
    }
  }
}
