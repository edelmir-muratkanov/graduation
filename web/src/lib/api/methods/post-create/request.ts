import type { CollectorType, MethodParameterGroup } from '@/types'

import { API } from '../../instance'

export interface PostCreateMethodRequest {
  name: string
  collectorTypes: CollectorType[]
  parameters: {
    propertyId: string
    firstParameters?: MethodParameterGroup
    secondParameters?: MethodParameterGroup
  }[]
}

export type PostCreateMethodRequestConfig =
  RequestConfig<PostCreateMethodRequest>

export const postCreateMethod = ({
  params,
  config,
}: PostCreateMethodRequestConfig) => API.post('methods', params, config)
