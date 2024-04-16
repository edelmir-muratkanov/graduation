import { useMutation } from '@tanstack/react-query'

import type { PostLoginRequestConfig } from './request'
import { postLogin } from './request'

export const usePostLoginMutation = (
  settings?: MutationSettings<PostLoginRequestConfig, typeof postLogin>,
) =>
  useMutation({
    mutationKey: ['postLogin'],
    mutationFn: ({ params, config }) =>
      postLogin({ params, config: { ...settings?.config, ...config } }),
    ...settings?.options,
  })
