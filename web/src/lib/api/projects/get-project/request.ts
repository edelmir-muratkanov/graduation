import type { Project } from '@/types'

import { API } from '../../instance'

export type GetProjectRequestConfig = RequestConfig | void

export const getProject = (
  projectId: string,
  params?: GetProjectRequestConfig,
) => API.get<Project>(`projects/${projectId}`, params?.config)
