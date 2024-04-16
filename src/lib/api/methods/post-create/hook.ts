import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import type { PostCreateMethodRequestConfig } from './request'
import { postCreateMethod } from './request'

export const usePostCreateMethodMutation = (
  settings?: MutationSettings<
    PostCreateMethodRequestConfig,
    typeof postCreateMethod
  >,
) =>
  useMutation({
    mutationKey: ['postCreateMethod'],
    mutationFn: ({ params, config }) =>
      postCreateMethod({ params, config: { ...settings?.config, ...config } }),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['methods'] }),
    ...settings?.options,
  })
