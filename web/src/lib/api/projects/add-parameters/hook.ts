import { useMutation } from '@tanstack/react-query'

import type { PostAddProjectParametersRequestConfig } from './request'
import { postAddProjectParameters } from './request'

export const usePostAddProjectParametersMutation = (
  projectId: string,
  settings?: MutationSettings<
    PostAddProjectParametersRequestConfig,
    typeof postAddProjectParameters
  >,
) =>
  useMutation({
    mutationKey: ['postAddMethodParameter'],
    mutationFn: ({ params, config }) =>
      postAddProjectParameters(projectId, {
        params,
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
