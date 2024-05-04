import { API } from '../../instance'

export type PostAddProjectMembersRequestConfig = RequestConfig<string[]>

export const postAddProjectMembers = (
  projectId: string,
  { params, config }: PostAddProjectMembersRequestConfig,
) => API.post(`projects/${projectId}/members`, params, config)
