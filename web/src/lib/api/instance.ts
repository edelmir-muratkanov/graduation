import axios from 'axios'

import { STORAGE_KEYS } from '../constants'

export const API = axios.create({
  baseURL: '/api',
  withCredentials: true,
})

API.interceptors.request.use(config => {
  const token = localStorage.getItem(STORAGE_KEYS.AccessToken)
  config.headers.Authorization = `Bearer ${token}`

  return config
})
