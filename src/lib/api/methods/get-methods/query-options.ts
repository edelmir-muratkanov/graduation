import { queryOptions } from '@tanstack/react-query'

import { getMethods } from './request'

export const getMethodsQueryOptions = (
  settings?: QuerySettings<typeof getMethods>,
) =>
  queryOptions({
    queryKey: ['methods'],
    queryFn: ({ signal }) =>
      getMethods({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })
