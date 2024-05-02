import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import {
  patchUpdateMethodBase,
  type PatchUpdateMethodBaseRequestConfig,
} from './request'

export const usePatchUpdateMethodBaseMutation = (
  methodId: string,
  settings?: MutationSettings<
    PatchUpdateMethodBaseRequestConfig,
    typeof patchUpdateMethodBase
  >,
) =>
  useMutation({
    mutationKey: ['patchUpdateMethodBase'],
    mutationFn: ({ params, config }) =>
      patchUpdateMethodBase(methodId, {
        params,
        config: { ...settings?.config, ...config },
      }),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['methods'] }),
    ...settings?.options,
  })
