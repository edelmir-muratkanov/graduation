import type { User } from '@/types'

import { API } from '../../instance'

export type PostLoginParams = {
  email: string
  password: string
}

export type PostLoginResponse = User & {
  token: string
}

export type PostLoginRequestConfig = RequestConfig<PostLoginParams>

export const postLogin = ({ params, config }: PostLoginRequestConfig) =>
  API.post<PostLoginResponse>('auth/login', params, config)
