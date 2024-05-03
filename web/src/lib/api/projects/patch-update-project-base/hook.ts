import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import {
  patchUpdateProjectBase,
  type PatchUpdateProjectBaseRequestConfig,
} from './request'

export const usePatchUpdateProjectBaseMutation = (
  projectId: string,
  settings?: MutationSettings<
    PatchUpdateProjectBaseRequestConfig,
    typeof patchUpdateProjectBase
  >,
) =>
  useMutation({
    mutationKey: ['patchUpdateProjectBase'],
    mutationFn: ({ params, config }) =>
      patchUpdateProjectBase(projectId, {
        params,
        config: { ...settings?.config, ...config },
      }),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['projects'] }),
    ...settings?.options,
  })
