import { API } from '../../instance'

export type GetProjectCalculationsRequestConfig = RequestConfig | void

export type ProjectCalculationsResponse = {
  name: string
  ratio: number
  applicability: string
  items: {
    name: string
    ratio: number
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
