import { keepPreviousData, useSuspenseQuery } from '@tanstack/react-query'

import { getPropertiesQueryOptions } from './query-options'
import type { getProperties } from './request'

export const useGetPropertiesQuery = (
  settings?: QuerySettings<typeof getProperties>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getPropertiesQueryOptions(settings),
    ...settings?.options,
  })
