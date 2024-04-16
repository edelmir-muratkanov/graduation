import { createLazyFileRoute } from '@tanstack/react-router'

import { ProjectsPage } from '@/pages/projects/page'

export const Route = createLazyFileRoute('/projects/')({
  component: ProjectsPage,
})
