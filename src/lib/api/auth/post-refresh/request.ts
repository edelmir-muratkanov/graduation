import { API } from '../../instance'

export interface PostRefreshResponse {
  user: User
  token: string
}

export type PostRefreshRequestConfig = RequestConfig

export const postRefresh = ({ config }: PostRefreshRequestConfig) =>
  API.post<PostRefreshResponse>('auth/refresh', undefined, config)
