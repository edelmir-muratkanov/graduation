import { API } from '../../instance'

export type PostLogoutRequestConfig = RequestConfig

export const postLogout = ({ config }: PostLogoutRequestConfig) =>
  API.post('auth/logout', undefined, config)
