import { API } from '../../instance'

export interface PostRegisterParams {
  email: string
  password: string
}

export interface PostRegisterResponse {
  user: User
  token: string
}

export type PostRegisterRequestConfig = RequestConfig<PostRegisterParams>

export const postRegister = ({ params, config }: PostRegisterRequestConfig) =>
  API.post<PostRegisterResponse>('auth/register', params, config)
