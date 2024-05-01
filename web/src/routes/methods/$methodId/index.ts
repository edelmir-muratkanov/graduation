import { createFileRoute } from '@tanstack/react-router'

import { getMethodQueryOptions, getPropertiesQueryOptions } from '@/lib/api'
import { MethodLoading } from '@/pages/method/loading'

export const Route = createFileRoute('/methods/$methodId/')({
  loader: ({ context: { queryClient }, params: { methodId } }) => {
    queryClient.ensureQueryData(getMethodQueryOptions(methodId))
    queryClient.ensureQueryData(
      getPropertiesQueryOptions({
        config: { params: { pageSize: 10, pageNumber: 1 } },
      }),
    )
  },

  pendingComponent: MethodLoading,
})
