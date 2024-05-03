import { API } from '../../instance'

export type DeleteProjectRequestConfig = RequestConfig

export const deleteProject = (
  projectId: string,
  { config }: DeleteProjectRequestConfig,
) => API.delete(`projects/${projectId}`, config)
