import { API } from '../../instance'

export interface PostLoginParams {
  email: string
  password: string
}

export type PostLoginRequestConfig = RequestConfig<PostLoginParams>

export const postLogin = ({ params, config }: PostLoginRequestConfig) =>
  API.post<{ accessToken: string }>('auth/login', params, config)
