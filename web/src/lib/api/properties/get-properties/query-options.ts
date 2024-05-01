import { queryOptions } from '@tanstack/react-query'

import { getProperties } from './request'

export const getPropertiesQueryOptions = (
  settings?: QuerySettings<typeof getProperties>,
) =>
  queryOptions({
    queryKey: ['properties', settings?.config?.params],
    queryFn: ({ signal }) =>
      getProperties({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })
