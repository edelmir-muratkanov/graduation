import { API } from '../../instance'

export interface PostCreatePropertyParams {
  name: string
}

export type PostCreatePropertyRequestConfig =
  RequestConfig<PostCreatePropertyParams>
export const postCreateProperty = ({
  params,
  config,
}: PostCreatePropertyRequestConfig) => API.post('properties', params, config)
