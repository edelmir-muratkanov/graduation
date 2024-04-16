import { API } from '../../instance'

type Group = {
  x: number
  xMin: number
  xMax: number
}

type Parameters = {
  first?: Group
  second?: Group
}

export interface PostCreateMethodParams {
  name: string
  collectorTypes: CollectorType[]
  data: {
    propertyId: string
    parameters: Parameters
  }[]
}

export type PostCreateMethodRequestConfig =
  RequestConfig<PostCreateMethodParams>

export const postCreateMethod = ({
  params,
  config,
}: PostCreateMethodRequestConfig) => API.post('methods', params, config)
