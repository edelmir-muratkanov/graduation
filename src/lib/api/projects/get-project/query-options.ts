import { queryOptions } from '@tanstack/react-query'

import { getProject } from './request'

export const getProjectQueryOptions = (
  projectId: string,
  settings?: QuerySettings<typeof getProject>,
) =>
  queryOptions({
    queryKey: ['projects', projectId],
    queryFn: ({ signal }) =>
      getProject(projectId, { config: { signal, ...settings?.config } }),
  })
