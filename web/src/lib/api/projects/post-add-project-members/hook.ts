import { useMutation } from '@tanstack/react-query'

import type { PostAddProjectMembersRequestConfig } from './request'
import { postAddProjectMembers } from './request'

export const usePostAddProjectMembersMutation = (
  projectId: string,
  settings?: MutationSettings<
    PostAddProjectMembersRequestConfig,
    typeof postAddProjectMembers
  >,
) =>
  useMutation({
    mutationKey: ['postAddProjectMembers'],
    mutationFn: ({ params, config }) =>
      postAddProjectMembers(projectId, {
        params,
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
