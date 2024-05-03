import { API } from '../../instance'

export type DeleteProjectMethodRequestConfig = RequestConfig

export const deleteProjectMethod = (
  projectId: string,
  methodId: string,
  { config }: DeleteProjectMethodRequestConfig,
) => API.delete(`projects/${projectId}/methods/${methodId}`, config)
