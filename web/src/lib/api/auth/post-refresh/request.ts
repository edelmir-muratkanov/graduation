import { API } from '../../instance'

export type PostRefreshRequestConfig = RequestConfig

export type PostRefreshResponse = {
  token: string
}

export const postRefresh = ({ config }: PostRefreshRequestConfig) =>
  API.post<PostRefreshResponse>('auth/refresh', undefined, config)
