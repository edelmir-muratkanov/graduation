import { queryOptions } from '@tanstack/react-query'

import { getProjects } from './request'

export const getProjectsQueryOptions = (
  settings?: QuerySettings<typeof getProjects>,
) =>
  queryOptions({
    queryKey: ['projects', settings?.config?.params],
    queryFn: ({ signal }) =>
      getProjects({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })
