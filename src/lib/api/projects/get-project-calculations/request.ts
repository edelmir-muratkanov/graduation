import { API } from '../../instance'

export type GetProjectCalculationsRequestConfig = RequestConfig | void

export type ProjectCalculationsResponse = {
  name: Method['name']
  result: string
  totalRatio: number
  paramsRatio: {
    ratio: number
    name: Pick<Property, 'name'>
  }[]
}

export const getProjectCalculations = (
  projectId: string,
  params?: GetProjectCalculationsRequestConfig,
) =>
  API.get<ProjectCalculationsResponse[]>(
    `projects/${projectId}/calculations`,
    params?.config,
  )
