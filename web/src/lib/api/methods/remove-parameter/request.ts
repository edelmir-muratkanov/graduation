import { API } from '../../instance'

export type DeleteMethodParameterRequestConfig = RequestConfig

export const deleteMethodParameter = (
  methodId: string,
  parameterId: string,
  { config }: DeleteMethodParameterRequestConfig,
) => API.delete(`methods/${methodId}/parameters/${parameterId}`, config)
