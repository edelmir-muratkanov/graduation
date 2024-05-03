import { useMutation } from '@tanstack/react-query'

import type { PostAddMethodParameterRequestConfig } from './request'
import { postAddMethodParameters } from './request'

export const usePostAddMethodParametersMutation = (
  methodId: string,
  settings?: MutationSettings<
    PostAddMethodParameterRequestConfig,
    typeof postAddMethodParameters
  >,
) =>
  useMutation({
    mutationKey: ['postAddMethodParameter'],
    mutationFn: ({ params, config }) =>
      postAddMethodParameters(methodId, {
        params,
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
