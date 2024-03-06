import { useMutation } from '@tanstack/react-query'

import type { PostRegisterRequestConfig } from '../../requests'
import { postRegister } from '../../requests'

export const usePostRegisterMutation = (
  settings?: MutationSettings<PostRegisterRequestConfig, typeof postRegister>,
) =>
  useMutation({
    mutationKey: ['postRegister'],
    mutationFn: ({ params, config }) =>
      postRegister({ params, config: { ...settings?.config, ...config } }),
    ...settings?.options,
  })
