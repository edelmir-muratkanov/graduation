import {
  keepPreviousData,
  queryOptions,
  useSuspenseQuery,
} from '@tanstack/react-query'

import { getProject } from '../../requests'

export const getProjectQueryOptions = (
  projectId: string,
  settings?: QuerySettings<typeof getProject>,
) =>
  queryOptions({
    queryKey: ['projects', projectId],
    queryFn: ({ signal }) =>
      getProject(projectId, { config: { signal, ...settings?.config } }),
  })

export const useGetProjectQuery = (
  projectId: string,
  settings?: QuerySettings<typeof getProject>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getProjectQueryOptions(projectId, settings),
    ...settings?.options,
  })
