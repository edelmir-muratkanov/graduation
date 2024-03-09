import { createFileRoute } from '@tanstack/react-router'

import {
  getProjectCalculationsQueryOptions,
  getProjectQueryOptions,
} from '@/lib/api'
import { ProjectPage } from '@/pages/project/page'

export const Route = createFileRoute('/projects/$projectId/')({
  component: ProjectPage,
  loader: async ({ context, params }) => {
    await context.queryClient.ensureQueryData(
      getProjectQueryOptions(params.projectId),
    )

    await context.queryClient.ensureQueryData(
      getProjectCalculationsQueryOptions(params.projectId),
    )
  },
})
