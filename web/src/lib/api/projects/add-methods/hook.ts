import { useMutation } from '@tanstack/react-query'

import type { PostAddProjectMethodsRequestConfig } from './request'
import { postAddProjectMethods } from './request'

export const usePostAddProjectMethodsMutation = (
  projectId: string,
  settings?: MutationSettings<
    PostAddProjectMethodsRequestConfig,
    typeof postAddProjectMethods
  >,
) =>
  useMutation({
    mutationKey: ['postAddProjectMethods'],
    mutationFn: ({ params, config }) =>
      postAddProjectMethods(projectId, {
        params,
        config: { ...settings?.config, ...config },
      }),

    ...settings?.options,
  })
