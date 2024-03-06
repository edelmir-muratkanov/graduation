/* eslint-disable eslint-comments/disable-enable-pair */
/* eslint-disable no-underscore-dangle */
import axios from 'axios'

import { STORAGE_KEYS } from '../constants'

export const API = axios.create({
  baseURL: '/api',
  withCredentials: true,
})

API.interceptors.response.use(
  res => res,
  async error => {
    const originalRequest = error.config

    if (error.response?.status === 401 && !originalRequest?._retry) {
      originalRequest._retry = true
      const { data } = await API.post<{ token: string }>('auth/refresh')
      localStorage.setItem(STORAGE_KEYS.AccessToken, data.token)
      return API(originalRequest)
    }

    return Promise.reject(error)
  },
)
