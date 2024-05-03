import type { MethodParameterGroup } from '@/types'

import { API } from '../../instance'

export interface PostAddMethodParametersRequest {
  propertyId: string
  first: MethodParameterGroup | null
  second: MethodParameterGroup | null
}

export type PostAddMethodParameterRequestConfig = RequestConfig<
  PostAddMethodParametersRequest[]
>

export const postAddMethodParameters = (
  methodId: string,
  { params, config }: PostAddMethodParameterRequestConfig,
) => API.post(`methods/${methodId}/parameters`, params, config)
