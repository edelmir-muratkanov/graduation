import { createLazyFileRoute } from '@tanstack/react-router'

import { PropertiesPage } from '@/pages/properties/page'

export const Route = createLazyFileRoute('/properties/')({
  component: PropertiesPage,
})
