import { createLazyFileRoute } from '@tanstack/react-router'

import { MethodPage } from '@/pages/method/page'

export const Route = createLazyFileRoute('/methods/$methodId/')({
  component: MethodPage,
})
