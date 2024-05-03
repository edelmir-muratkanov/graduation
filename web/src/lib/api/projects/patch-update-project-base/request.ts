import type { CollectorType, ProjectType } from '@/types'

import { API } from '../../instance'

export interface PatchUpdateProjectBaseRequest {
  name: string | null
  country: string | null
  operator: string | null
  type: ProjectType | null
  collectorType: CollectorType | null
}

export type PatchUpdateProjectBaseRequestConfig =
  RequestConfig<PatchUpdateProjectBaseRequest>

export const patchUpdateProjectBase = (
  projectId: string,
  { params, config }: PatchUpdateProjectBaseRequestConfig,
) => API.patch(`projects/${projectId}`, params, config)
