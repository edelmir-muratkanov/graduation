import { createLazyFileRoute } from '@tanstack/react-router'

import { MethodsPage } from '@/pages/methods/page'

export const Route = createLazyFileRoute('/methods/')({
  component: MethodsPage,
})
