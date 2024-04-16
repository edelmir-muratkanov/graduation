import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getMethodsQueryOptions } from './query-options'
import type { getMethods } from './request'

export const useGetMethodsQuery = (
  settings?: QuerySettings<typeof getMethods>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getMethodsQueryOptions(settings),
    ...settings?.options,
  })
