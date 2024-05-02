import type { CollectorType } from '@/types'

import { API } from '../../instance'

export interface PatchUpdateMethodBaseRequest {
  name: string | null
  collectorTypes: CollectorType[] | null
}

export type PatchUpdateMethodBaseRequestConfig =
  RequestConfig<PatchUpdateMethodBaseRequest>

export const patchUpdateMethodBase = (
  methodId: string,
  { params, config }: PatchUpdateMethodBaseRequestConfig,
) => API.patch(`methods/${methodId}`, params, config)
