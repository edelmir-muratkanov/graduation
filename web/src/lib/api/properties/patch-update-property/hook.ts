import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import {
  patchUpdateProperty,
  type PatchUpdatePropertyRequestConfig,
} from './request'

export const usePatchUpdatePropertyMutation = (
  methodId: string,
  settings?: MutationSettings<
    PatchUpdatePropertyRequestConfig,
    typeof patchUpdateProperty
  >,
) =>
  useMutation({
    mutationKey: ['patchUpdateProperty'],
    mutationFn: ({ params, config }) =>
      patchUpdateProperty(methodId, {
        params,
        config: { ...settings?.config, ...config },
      }),
    onSuccess: () =>
      queryClient.invalidateQueries({ queryKey: ['properties'] }),
    ...settings?.options,
  })
