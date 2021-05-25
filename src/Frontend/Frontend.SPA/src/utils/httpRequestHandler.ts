import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
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
  private static _api = axios.create({
    baseURL: process.env.VUE_APP_GATEWAY_HOST,
  });

  public static async get<TResponse>(path: string, config?: AxiosRequestConfig): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.get(path, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public static async post<TRequest, TResponse>(
    path: string,
    body: TRequest,
    config?: AxiosRequestConfig
  ): Promise<TResponse> {
    try {
      const res = await this._api.post(path, body, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public static async put<TRequest, TResponse>(
    path: string,
    body: TRequest,
    config?: AxiosRequestConfig
  ): Promise<AxiosResponse<TResponse>> {
    try {
      const res = await this._api.put(path, body, config);
      return res.data;
    } catch (err) {
      throw this.checkStatusCode(err);
    }
  }

  public static async delete<TRequest, TResponse>(
    path: string,
    body: TRequest,
    config?: AxiosRequestConfig
  ): Promise<AxiosResponse<TResponse>> {
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

  private static checkStatusCode(err: AxiosError<any>): HttpException {
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
