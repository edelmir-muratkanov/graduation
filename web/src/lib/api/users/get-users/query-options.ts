import { queryOptions } from '@tanstack/react-query'

import { getUsers } from './request'

export const getUsersQueryOptions = (
  settings?: QuerySettings<typeof getUsers>,
) =>
  queryOptions({
    queryKey: ['users', settings?.config?.params],
    queryFn: ({ signal }) =>
      getUsers({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })
