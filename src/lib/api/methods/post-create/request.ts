import { API } from '../../instance'

type Group = {
  x: number
  xMin: number
  xMax: number
}

type Parameter =
  | {
      values: number[]
    }
  | {
      first?: Group
      second?: Group
    }

export interface PostCreateMethodParams {
  name: string
  data: {
    propertyId: string
    parameters: Parameter[]
  }
}

export type PostCreateMethodRequestConfig =
  RequestConfig<PostCreateMethodParams>

export const postCreateMethod = ({
  params,
  config,
}: PostCreateMethodRequestConfig) => API.post('methods', params, config)
