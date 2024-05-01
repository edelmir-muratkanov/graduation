import type { Property } from '@/types'

import { API } from '../../instance'

export type GetPropertiesRequestConfig = RequestConfig | void

export type GetPropertiesResponse = Property

export const getProperties = (params?: GetPropertiesRequestConfig) =>
  API.get<BasePaginatedResponse<GetPropertiesResponse>>(
    'properties',
    params?.config,
  )
