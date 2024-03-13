import { createLazyFileRoute } from '@tanstack/react-router'

import { ProjectLoading } from '@/pages/project/loading'
import { ProjectPage } from '@/pages/project/page'

export const Route = createLazyFileRoute('/projects/$projectId/')({
  component: ProjectPage,
  pendingComponent: ProjectLoading,
})
