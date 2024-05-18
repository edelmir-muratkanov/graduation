import { createFileRoute, notFound } from '@tanstack/react-router'
import { isAxiosError } from 'axios'

import { getMethodQueryOptions, getPropertiesQueryOptions } from '@/lib/api'
import { MethodLoading } from '@/pages/method/loading'

export const Route = createFileRoute('/methods/$methodId/')({
  loader: ({ context: { queryClient }, params: { methodId } }) =>
    Promise.all([
      queryClient.ensureQueryData(getMethodQueryOptions(methodId)),
      queryClient.ensureQueryData(
        getPropertiesQueryOptions({
          config: { params: { pageSize: 10, pageNumber: 1 } },
        }),
      ),
    ]).catch(err => {
      if (isAxiosError(err) && err.response?.status === 404) {
        throw notFound()
      }

      throw err
    }),
  pendingComponent: MethodLoading,
})
