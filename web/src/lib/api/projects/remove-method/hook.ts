import { useMutation } from '@tanstack/react-query'

import type { DeleteProjectMethodRequestConfig } from './request'
import { deleteProjectMethod } from './request'

export const useDeleteProjectMethodMutation = (
  projectId: string,
  methodId: string,
  settings?: MutationSettings<
    DeleteProjectMethodRequestConfig,
    typeof deleteProjectMethod
  >,
) =>
  useMutation({
    mutationKey: ['deleteProjectMethod'],
    mutationFn: ({ config }) =>
      deleteProjectMethod(projectId, methodId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
