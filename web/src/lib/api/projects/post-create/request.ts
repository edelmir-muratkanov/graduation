import type { CollectorType, ProjectType } from '@/types'

import { API } from '../../instance'

export interface PostCreateProjectParams {
  name: string
  country: string
  operator: string
  type: ProjectType
  collectorType: CollectorType
  methodIds: string[]
  parameters: { propertyId: string; value: number }[]
}

export type PostCreateProjectRequestConfig =
  RequestConfig<PostCreateProjectParams>

export const postCreateProject = ({
  params,
  config,
}: PostCreateProjectRequestConfig) => API.post('projects', params, config)
