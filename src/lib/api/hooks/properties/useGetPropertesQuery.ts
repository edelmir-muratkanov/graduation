import {
  keepPreviousData,
  queryOptions,
  useSuspenseQuery,
} from '@tanstack/react-query'

import { getProperties } from '../../requests'

export const getPropertiesQueryOptions = (
  settings?: QuerySettings<typeof getProperties>,
) =>
  queryOptions({
    queryKey: ['properties'],
    queryFn: ({ signal }) =>
      getProperties({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })

export const useGetPropertiesQuery = (
  settings?: QuerySettings<typeof getProperties>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getPropertiesQueryOptions(settings),
    ...settings?.options,
  })
