import { queryOptions } from '@tanstack/react-query'

import { getMethod } from './request'

export const getMethodQueryOptions = (
  methodId: string,
  settings?: QuerySettings<typeof getMethod>,
) =>
  queryOptions({
    queryKey: ['methods', methodId],
    queryFn: ({ signal }) =>
      getMethod(methodId, { config: { signal, ...settings?.config } }),
  })
