import { createLazyFileRoute } from '@tanstack/react-router'

import { CreateProjectPage } from '@/pages/create-project/page'

export const Route = createLazyFileRoute('/_user/projects/new/')({
  component: CreateProjectPage,
})
