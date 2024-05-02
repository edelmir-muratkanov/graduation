import { useMutation } from '@tanstack/react-query'

import type { DeleteMethodRequestConfig } from './request'
import { deleteMethod } from './request'

export const useDeleteMethodMutation = (
  methodId: string,
  settings?: MutationSettings<DeleteMethodRequestConfig, typeof deleteMethod>,
) =>
  useMutation({
    mutationKey: ['deleteMethod'],
    mutationFn: ({ config }) =>
      deleteMethod(methodId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
