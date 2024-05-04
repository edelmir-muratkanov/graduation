import type { User } from '@/types'

import { API } from '../../instance'

export type GetUsersRequestConfig = RequestConfig | void

export type GetUsersResponse = Omit<User, 'role'>

export const getUsers = (params?: GetUsersRequestConfig) =>
  API.get<BasePaginatedResponse<GetUsersResponse>>('users', params?.config)
