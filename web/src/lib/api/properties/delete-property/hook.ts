import { useMutation } from '@tanstack/react-query'

import type { DeletePropertyRequestConfig } from './request'
import { deleteProperty } from './request'

export const useDeletePropertyMutation = (
  propertyId: string,
  settings?: MutationSettings<
    DeletePropertyRequestConfig,
    typeof deleteProperty
  >,
) =>
  useMutation({
    mutationKey: ['deleteProperty'],
    mutationFn: ({ config }) =>
      deleteProperty(propertyId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
