import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getProjectQueryOptions } from './query-options'
import type { getProject } from './request'

export const useGetProjectQuery = (
  projectId: string,
  settings?: QuerySettings<typeof getProject>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getProjectQueryOptions(projectId, settings),
    ...settings?.options,
  })
