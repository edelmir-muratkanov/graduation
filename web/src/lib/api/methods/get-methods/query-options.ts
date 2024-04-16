import { queryOptions } from '@tanstack/react-query'

import { getMethods } from './request'

export const getMethodsQueryOptions = (
  settings?: QuerySettings<typeof getMethods>,
) =>
  queryOptions({
    queryKey: ['methods', settings?.config?.params],
    queryFn: ({ signal }) =>
      getMethods({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })
