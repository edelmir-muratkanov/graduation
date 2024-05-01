import type { User } from '@/types'

import { API } from '../../instance'

export type GetProfileRequestConfig = RequestConfig | void

export type GetProfileResponse = User & {
  token: string
}

export const getProfile = (params?: GetProfileRequestConfig) =>
  API.get<GetProfileResponse>('auth/profile', params?.config)
