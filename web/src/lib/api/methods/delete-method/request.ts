import { API } from '../../instance'

export type DeleteMethodRequestConfig = RequestConfig

export const deleteMethod = (
  methodId: string,
  { config }: DeleteMethodRequestConfig,
) => API.delete(`methods/${methodId}`, config)
