import { createLazyFileRoute } from '@tanstack/react-router'

import { CreateMethodPage } from '@/pages/create-method/page'

export const Route = createLazyFileRoute('/_admin/methods/new/')({
  component: CreateMethodPage,
})
