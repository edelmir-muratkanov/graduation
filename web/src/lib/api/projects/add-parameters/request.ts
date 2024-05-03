import { API } from '../../instance'

export interface PostAddProjectParametersRequest {
  propertyId: string
  value: number
}

export type PostAddProjectParametersRequestConfig = RequestConfig<
  PostAddProjectParametersRequest[]
>

export const postAddProjectParameters = (
  projectId: string,
  { params, config }: PostAddProjectParametersRequestConfig,
) => API.post(`projects/${projectId}/parameters`, params, config)
