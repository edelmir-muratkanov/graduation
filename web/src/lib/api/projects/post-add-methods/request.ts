import { API } from '../../instance'

export type PostAddProjectMethodsRequestConfig = RequestConfig<string[]>

export const postAddProjectMethods = (
  projectId: string,
  { params, config }: PostAddProjectMethodsRequestConfig,
) => API.post(`projects/${projectId}/methods`, params, config)
