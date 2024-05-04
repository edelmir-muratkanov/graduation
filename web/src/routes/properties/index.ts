import { createFileRoute } from '@tanstack/react-router'

import { getPropertiesQueryOptions } from '@/lib/api'
import { PropertiesLoading } from '@/pages/properties/loading'

export const Route = createFileRoute('/properties/')({
  loader: ({ context }) =>
    context.queryClient.ensureQueryData(
      getPropertiesQueryOptions({
        config: {
          params: {
            pageNumber: 1,
            pageSize: 10,
            searchTerm: '',
            sortColumn: 'name',
          },
        },
      }),
    ),
  pendingComponent: PropertiesLoading,
})
