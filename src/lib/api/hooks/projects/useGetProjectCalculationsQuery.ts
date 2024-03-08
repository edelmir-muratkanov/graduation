import {
  keepPreviousData,
  queryOptions,
  useSuspenseQuery,
} from '@tanstack/react-query'

import { getProjectCalculations } from '../../requests'

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

export const useGetProjectCalculationsQuery = (
  projectId: string,
  settings?: QuerySettings<typeof getProjectCalculations>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getProjectCalculationsQueryOptions(projectId, settings),
    ...settings?.options,
  })
