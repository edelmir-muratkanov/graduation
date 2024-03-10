import { createFileRoute } from '@tanstack/react-router'

import {
  getProjectCalculationsQueryOptions,
  getProjectQueryOptions,
} from '@/lib/api'

export const Route = createFileRoute('/projects/$projectId/')({
  loader: async ({ context, params }) => {
    await context.queryClient.ensureQueryData(
      getProjectQueryOptions(params.projectId),
    )

    await context.queryClient.ensureQueryData(
      getProjectCalculationsQueryOptions(params.projectId),
    )
  },
})
