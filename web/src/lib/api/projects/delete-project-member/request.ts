import { API } from '../../instance'

export type DeleteProjectMemberRequestConfig = RequestConfig

export const deleteProjectMember = (
  projectId: string,
  memberId: string,
  { config }: DeleteProjectMemberRequestConfig,
) => API.delete(`projects/${projectId}/members/${memberId}`, config)
