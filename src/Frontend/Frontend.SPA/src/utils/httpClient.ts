import { auth } from '@/plugins/firebase';
import axios, { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
import { StatusCodes } from 'http-status-codes';
import BadRequestException from './exceptions/badRequest.exception';
import InternalServerException from './exceptions/internalServer.exception';
import UnauthorizedException from './exceptions/unauthorized.exception';

class HttpClient {
  private instance: AxiosInstance | null = null;

  private get httpClient(): AxiosInstance {
    return this.instance != null ? this.instance : this.initHttpClient();
  }

  initHttpClient() {
    const httpClient = axios.create({
      baseURL: process.env.VUE_APP_GATEWAY_HOST,
    });

    this.instance = httpClient;
    this.initializeRequestInterceptor();
    this.initializeResponseInterceptor();

    return httpClient;
  }

  get<T = any, R = AxiosResponse<T>>(path: string, config?: AxiosRequestConfig): Promise<R> {
    return this.httpClient.get(path, config).then((res: AxiosResponse<R>) => {
      return res.data;
    });
  }

  post<T = any, R = AxiosResponse<T>>(path: string, body: T, config?: AxiosRequestConfig): Promise<R> {
    return this.httpClient.post(path, body, config).then((res: AxiosResponse<R>) => {
      return res.data;
    });
  }

  put<T = any, R = AxiosResponse<T>>(path: string, body: T, config?: AxiosRequestConfig): Promise<R> {
    return this.httpClient.put(path, body, config).then((res: AxiosResponse<R>) => {
      return res.data;
    });
  }

  delete<T = any, R = AxiosResponse<T>>(path: string, config?: AxiosRequestConfig): Promise<R> {
    return this.httpClient.delete(path, config);
  }

  private initializeRequestInterceptor = async () => {
    this.httpClient.interceptors.request.use(
      async function (config) {
        const idToken = await auth.currentUser!.getIdToken();
        config.headers.Authorization = idToken ? `Bearer ${idToken}` : '';
        return config;
      },
      function (error) {
        return Promise.reject(error);
      }
    );
  };

  private initializeResponseInterceptor = async () => {
    this.httpClient.interceptors.response.use(
      (response) => response,
      (error: AxiosError<any>) => {
        if (!error || !error.response) return new InternalServerException();
        return this.handleError(error.response);
      }
    );
  };

  // Handle global app errors
  private handleError(error: AxiosResponse<any>) {
    const apiErrorResponse: string[] = error.data.errors;
    const internalServerError: string = error.data.message;

    switch (error.status) {
      case StatusCodes.INTERNAL_SERVER_ERROR: {
        throw new InternalServerException(internalServerError);
      }
      case StatusCodes.UNAUTHORIZED: {
        throw new UnauthorizedException(apiErrorResponse[0]);
      }
      case StatusCodes.BAD_REQUEST: {
        throw new BadRequestException(apiErrorResponse[0]);
      }
      default: {
        throw new InternalServerException();
      }
    }
  }
}

export const httpClient = new HttpClient();
