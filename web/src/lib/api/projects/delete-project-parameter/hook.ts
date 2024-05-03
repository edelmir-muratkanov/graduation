import { useMutation } from '@tanstack/react-query'

import type { DeleteProjectParameterRequestConfig } from './request'
import { deleteProjectParameter } from './request'

export const useDeleteProjectParameterMutation = (
  projectId: string,
  parameterId: string,
  settings?: MutationSettings<
    DeleteProjectParameterRequestConfig,
    typeof deleteProjectParameter
  >,
) =>
  useMutation({
    mutationKey: ['deleteProjectParameter'],
    mutationFn: ({ config }) =>
      deleteProjectParameter(projectId, parameterId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
