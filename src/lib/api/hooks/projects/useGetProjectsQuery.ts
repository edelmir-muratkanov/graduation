import { keepPreviousData, queryOptions, useQuery } from '@tanstack/react-query'

import { getProjects } from '../../requests'

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

export const useGetProjectsQuery = (
  settings?: QuerySettings<typeof getProjects>,
) =>
  useQuery({
    placeholderData: keepPreviousData,
    ...getProjectsQueryOptions(settings),
    ...settings?.options,
  })
