import { useMutation } from '@tanstack/react-query'

import type { PostLoginRequestConfig } from '../../requests/auth/login'
import { postLogin } from '../../requests/auth/login'

export const usePostLoginMutation = (
  settings?: MutationSettings<PostLoginRequestConfig, typeof postLogin>,
) =>
  useMutation({
    mutationKey: ['postLogin'],
    mutationFn: ({ params, config }) =>
      postLogin({ params, config: { ...settings?.config, ...config } }),
    ...settings?.options,
  })
