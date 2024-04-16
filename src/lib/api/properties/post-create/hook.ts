import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import type { PostCreatePropertyRequestConfig } from './request'
import { postCreateProperty } from './request'

export const usePostCreatePropertyMutation = (
  settings?: MutationSettings<
    PostCreatePropertyRequestConfig,
    typeof postCreateProperty
  >,
) =>
  useMutation({
    mutationKey: ['postCreateProperty'],
    mutationFn: ({ params, config }) =>
      postCreateProperty({
        params,
        config: { ...settings?.config, ...config },
      }),
    onSuccess: () =>
      queryClient.invalidateQueries({ queryKey: ['properties'] }),
    ...settings?.options,
  })
