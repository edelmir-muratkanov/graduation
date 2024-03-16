import { useMutation } from '@tanstack/react-query'

import { queryClient } from '@/lib/contexts'

import type { PostCreateProjectRequestConfig } from './request'
import { postCreateProject } from './request'

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
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['projects'] }),
    ...settings?.options,
  })
