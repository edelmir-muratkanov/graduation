import { useMutation } from '@tanstack/react-query'

import type { PostLogoutRequestConfig } from './request'
import { postLogout } from './request'

export const usePostLogoutMutation = (
  settings?: MutationSettings<PostLogoutRequestConfig, typeof postLogout>,
) =>
  useMutation({
    mutationKey: ['postLogin'],
    mutationFn: ({ config }) =>
      postLogout({ config: { ...settings?.config, ...config } }),
    ...settings?.options,
  })
