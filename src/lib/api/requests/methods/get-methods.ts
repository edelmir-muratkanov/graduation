import { API } from '../../instance'

export type GetMethodsRequestConfig = RequestConfig | void

export interface GetMethodsResponse {
  count: number
  items: Pick<Method & MethodStatistic, 'id' | 'name' | '_count'>[]
}

export const getMethods = (params?: GetMethodsRequestConfig) =>
  API.get<GetMethodsResponse>('methods', params?.config)
