import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getUsersQueryOptions } from './query-options'
import type { getUsers } from './request'

export const useGetUsersQuery = (settings?: QuerySettings<typeof getUsers>) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getUsersQueryOptions(settings),
    ...settings?.options,
  })
