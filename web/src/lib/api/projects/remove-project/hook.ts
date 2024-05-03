import { useMutation } from '@tanstack/react-query'

import type { DeleteProjectRequestConfig } from './request'
import { deleteProject } from './request'

export const useDeleteProjectMutation = (
  projectId: string,
  settings?: MutationSettings<DeleteProjectRequestConfig, typeof deleteProject>,
) =>
  useMutation({
    mutationKey: ['deleteProject'],
    mutationFn: ({ config }) =>
      deleteProject(projectId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
