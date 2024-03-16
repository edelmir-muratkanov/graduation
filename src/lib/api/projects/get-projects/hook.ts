import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getProjectsQueryOptions } from './query-options'
import type { getProjects } from './request'

export const useGetProjectsQuery = (
  settings?: QuerySettings<typeof getProjects>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getProjectsQueryOptions(settings),
    ...settings?.options,
  })
