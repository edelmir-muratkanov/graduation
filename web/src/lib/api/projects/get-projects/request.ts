import type { Project } from '@/types'

import { API } from '../../instance'

export type GetProjectsRequestConfig = RequestConfig | void

export type GetProjectsResponse = Omit<
  Project,
  'parameters' | 'members' | 'ownerId' | 'methods'
>

export const getProjects = (params?: GetProjectsRequestConfig) =>
  API.get<BasePaginatedResponse<GetProjectsResponse>>(
    'projects',
    params?.config,
  )
