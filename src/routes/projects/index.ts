import { createFileRoute } from '@tanstack/react-router'

import { ProjectsLoading } from '@/pages/projects/loading'

export const Route = createFileRoute('/projects/')({
  pendingComponent: ProjectsLoading,
})
