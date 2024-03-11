import { useMutation } from '@tanstack/react-query'

import type { PostCreateProjectRequestConfig } from '../../requests'
import { postCreateProject } from '../../requests'

export const usePostCreateProjectMutation = (
  settings?: MutationSettings<
    PostCreateProjectRequestConfig,
    typeof postCreateProject
  >,
) =>
  useMutation({
    mutationKey: ['postCreateProject'],
    mutationFn: ({ params, config }) =>
      postCreateProject({ params, config: { ...settings?.config, ...config } }),
    ...settings?.options,
  })
