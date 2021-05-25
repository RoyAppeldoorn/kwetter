import { auth } from '@/plugins/firebase';
import axios, { AxiosInstance } from 'axios';

const api: AxiosInstance = axios.create({
  baseURL: process.env.VUE_APP_GATEWAY_HOST,
});

api.interceptors.request.use(async function (config) {
  const idToken = await auth.currentUser!.getIdToken();
  config.headers.Authorization = idToken ? `Bearer ${idToken}` : '';
  return config;
}, function (error) {
  return Promise.reject(error);
});

export default api;
