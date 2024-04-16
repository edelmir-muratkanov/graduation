import { queryOptions } from '@tanstack/react-query'

import { getProjectCalculations } from './request'

export const getProjectCalculationsQueryOptions = (
  projectId: string,
  settings?: QuerySettings<typeof getProjectCalculations>,
) =>
  queryOptions({
    queryKey: ['projects', projectId, 'calculations'],
    queryFn: ({ signal }) =>
      getProjectCalculations(projectId, {
        config: { signal, ...settings?.config },
      }),
  })
