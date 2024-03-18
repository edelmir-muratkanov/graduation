import { createFileRoute } from '@tanstack/react-router'

import { getPropertiesQueryOptions } from '@/lib/api'

export const Route = createFileRoute('/_admin/methods/new/')({
  loader: ({ context: { queryClient } }) =>
    queryClient.ensureQueryData(getPropertiesQueryOptions()),
})
