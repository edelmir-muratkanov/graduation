import type { User } from '@/types'

import { API } from '../../instance'

export type PostRegisterParams = {
  email: string
  password: string
}

export type PostRegisterResponse = User & {
  token: string
}

export type PostRegisterRequestConfig = RequestConfig<PostRegisterParams>

export const postRegister = ({ params, config }: PostRegisterRequestConfig) =>
  API.post<PostRegisterResponse>('auth/register', params, config)
