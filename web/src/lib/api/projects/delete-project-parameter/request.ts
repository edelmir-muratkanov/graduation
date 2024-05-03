import { API } from '../../instance'

export type DeleteProjectParameterRequestConfig = RequestConfig

export const deleteProjectParameter = (
  projectId: string,
  parameterId: string,
  { config }: DeleteProjectParameterRequestConfig,
) => API.delete(`projects/${projectId}/parameters/${parameterId}`, config)
