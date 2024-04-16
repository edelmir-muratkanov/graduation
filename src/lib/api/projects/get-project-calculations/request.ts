import { API } from '../../instance'

export type GetProjectCalculationsRequestConfig = RequestConfig | void

export type ProjectCalculationsResponse = {
  ratio: number
  method: Pick<Method, 'name' | 'id'>
  applicability: string
  items: {
    collectorType: CollectorType
    ratio: number
    property: Pick<Property, 'name'>
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
