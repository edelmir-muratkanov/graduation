import { createFileRoute, notFound } from '@tanstack/react-router'
import { isAxiosError } from 'axios'
import { z } from 'zod'

import {
  getProjectCalculationsQueryOptions,
  getProjectQueryOptions,
} from '@/lib/api'
import { ProjectLoading } from '@/pages/project/loading'

const projectSearchSchema = z.object({
  tab: z
    .enum(['info', 'calculations'] as const)
    .optional()
    .catch('info'),
})

export const Route = createFileRoute('/projects/$projectId/')({
  loader: ({ context: { queryClient }, params }) =>
    Promise.all([
      queryClient.ensureQueryData(getProjectQueryOptions(params.projectId)),
      queryClient.ensureQueryData(
        getProjectCalculationsQueryOptions(params.projectId),
      ),
    ]).catch(err => {
      if (isAxiosError(err) && err.response?.status === 404) {
        throw notFound()
      }

      throw err
    }),
  validateSearch: projectSearchSchema,
  pendingComponent: ProjectLoading,
})
