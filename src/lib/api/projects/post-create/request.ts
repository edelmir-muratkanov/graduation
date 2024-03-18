import { API } from '../../instance'

export interface PostCreateProjectParams {
  name: string
  country: string
  operator: string
  projectType: ProjectType
  collectorType: CollectorType
  methodIds: { methodId: string }[]
  parameters: { propertyId: string; value: number }[]
}

export type PostCreateProjectRequestConfig =
  RequestConfig<PostCreateProjectParams>

export const postCreateProject = ({
  params,
  config,
}: PostCreateProjectRequestConfig) => API.post('projects', params, config)
