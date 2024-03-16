import { API } from '../../instance'

export type GetProjectsRequestConfig = RequestConfig | void

type Item = Project & ProjectStatistic

export interface GetProjectsResponse {
  count: number
  items: Item[]
}

export const getProjects = (params?: GetProjectsRequestConfig) =>
  API.get<GetProjectsResponse>('projects', params?.config)
