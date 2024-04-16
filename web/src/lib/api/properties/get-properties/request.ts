import { API } from '../../instance'

export type GetPropertiesRequestConfig = RequestConfig | void

export interface GetPropertiesResponse {
  count: number
  items: (Property & PropertyStatistic)[]
}

export const getProperties = (params?: GetPropertiesRequestConfig) =>
  API.get<GetPropertiesResponse>('properties', params?.config)
