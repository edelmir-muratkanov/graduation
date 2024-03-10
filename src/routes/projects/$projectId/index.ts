import { createFileRoute } from '@tanstack/react-router'
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
  loader: ({ context, params }) => {
    context.queryClient.ensureQueryData(
      getProjectQueryOptions(params.projectId),
    )

    context.queryClient.ensureQueryData(
      getProjectCalculationsQueryOptions(params.projectId),
    )
  },
  validateSearch: projectSearchSchema,
  pendingComponent: ProjectLoading,
})
