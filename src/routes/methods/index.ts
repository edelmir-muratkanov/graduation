import { createFileRoute } from '@tanstack/react-router'

import { getMethodsQueryOptions } from '@/lib/api'
import { MethodsLoading } from '@/pages/methods/loading'

export const Route = createFileRoute('/methods/')({
  loader: ({ context: { queryClient } }) =>
    queryClient.ensureQueryData(getMethodsQueryOptions()),
  pendingComponent: MethodsLoading,
})
