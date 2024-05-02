import { useMutation } from '@tanstack/react-query'

import type { DeleteMethodParameterRequestConfig } from './request'
import { deleteMethodParameter } from './request'

export const useDeleteMethodParameterMutation = (
  methodId: string,
  parameterId: string,
  settings?: MutationSettings<
    DeleteMethodParameterRequestConfig,
    typeof deleteMethodParameter
  >,
) =>
  useMutation({
    mutationKey: ['postAddMethodParameter'],
    mutationFn: ({ config }) =>
      deleteMethodParameter(methodId, parameterId, {
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
