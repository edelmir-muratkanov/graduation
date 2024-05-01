import type { Method } from '@/types'

import { API } from '../../instance'

export type GetMethodsRequestConfig = RequestConfig | void

export type GetMethodsResponse = Omit<Method, 'parameters'>

export const getMethods = (params?: GetMethodsRequestConfig) =>
  API.get<BasePaginatedResponse<GetMethodsResponse>>('methods', params?.config)
