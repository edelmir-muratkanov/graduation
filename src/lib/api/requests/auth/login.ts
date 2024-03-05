import type { User } from '@/lib/interfaces'

import { API } from '../../instance'

export interface PostLoginParams {
  email: string
  password: string
}

export interface PostLoginResponse {
  user: User
  token: string
}

export type PostLoginRequestConfig = RequestConfig<PostLoginParams>

export const postLogin = ({ params, config }: PostLoginRequestConfig) =>
  API.post<PostLoginResponse>('auth/login', params, config)
