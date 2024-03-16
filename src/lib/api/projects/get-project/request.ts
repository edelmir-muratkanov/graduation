import { API } from '../../instance'

export type GetProjectRequestConfig = RequestConfig | void

export type ProjectResponse = Project & {
  users: Pick<User, 'id' | 'email'>[]
  parameters: {
    value: number
    property: Property
  }[]
  methods: Method[]
}

export const getProject = (
  projectId: string,
  params?: GetProjectRequestConfig,
) => API.get<ProjectResponse>(`projects/${projectId}`, params?.config)
