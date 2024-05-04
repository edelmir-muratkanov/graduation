import { useMutation } from '@tanstack/react-query'

import type { DeleteProjectMemberRequestConfig } from './request'
import { deleteProjectMember } from './request'

export const useDeleteProjectMemberMutation = (
  projectId: string,
  memberId: string,
  settings?: MutationSettings<
    DeleteProjectMemberRequestConfig,
    typeof deleteProjectMember
  >,
) =>
  useMutation({
    mutationKey: ['deleteProjectMember'],
    mutationFn: ({ config }) =>
      deleteProjectMember(projectId, memberId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
