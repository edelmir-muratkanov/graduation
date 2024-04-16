import { API } from '../../instance'

export type GetMethodRequestConfig = RequestConfig | void

export const getMethod = (methodId: string, params?: GetMethodRequestConfig) =>
  API.get<Method>(`methods/${methodId}`, params?.config)
