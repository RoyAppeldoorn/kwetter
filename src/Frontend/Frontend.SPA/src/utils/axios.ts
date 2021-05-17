import axios, { AxiosInstance } from 'axios';

const api: AxiosInstance = axios.create({
  baseURL: process.env.VUE_APP_GATEWAY_HOST,
});

export default api;
