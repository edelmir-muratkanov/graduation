import { API } from '../../instance'

export type GetProfileRequestConfig = RequestConfig | void

export const getProfile = (params?: GetProfileRequestConfig) =>
  API.get<User>('auth/profile', params?.config)
