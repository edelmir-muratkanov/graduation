import { createLazyFileRoute } from '@tanstack/react-router'

import { ProjectPage } from '@/pages/project/page'

export const Route = createLazyFileRoute('/projects/$projectId/')({
  component: ProjectPage,
})
