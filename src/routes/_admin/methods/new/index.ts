import { createFileRoute } from '@tanstack/react-router'

import { getPropertiesQueryOptions } from '@/lib/api'
import { CreateMethodLoading } from '@/pages/create-method/loading'

export const Route = createFileRoute('/_admin/methods/new/')({
  loader: ({ context: { queryClient } }) =>
    queryClient.ensureQueryData(getPropertiesQueryOptions()),

  pendingComponent: CreateMethodLoading,
})
