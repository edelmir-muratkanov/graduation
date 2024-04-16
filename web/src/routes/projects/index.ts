import { createFileRoute } from '@tanstack/react-router'

import { getProjectsQueryOptions } from '@/lib/api'
import { ProjectsLoading } from '@/pages/projects/loading'

export const Route = createFileRoute('/projects/')({
  loader: ({ context }) => {
    context.queryClient.ensureQueryData(
      getProjectsQueryOptions({ config: { params: { limit: 10, offset: 0 } } }),
    )
  },
  pendingComponent: ProjectsLoading,
})
