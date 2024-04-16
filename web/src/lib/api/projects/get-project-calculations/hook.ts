import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getProjectCalculationsQueryOptions } from './query-options'
import type { getProjectCalculations } from './request'

export const useGetProjectCalculationsQuery = (
  projectId: string,
  settings?: QuerySettings<typeof getProjectCalculations>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getProjectCalculationsQueryOptions(projectId, settings),
    ...settings?.options,
  })
