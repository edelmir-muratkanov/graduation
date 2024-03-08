import { keepPreviousData, useQuery } from '@tanstack/react-query'

import { getProjects } from '../../requests'

export const useGetProjectsQuery = (
  settings?: QuerySettings<typeof getProjects>,
) =>
  useQuery({
    queryKey: ['projects', settings?.config?.params],
    queryFn: ({ signal }) =>
      getProjects({
        config: {
          signal,
          ...settings?.config,
        },
      }),
    placeholderData: keepPreviousData,
    ...settings?.options,
  })
