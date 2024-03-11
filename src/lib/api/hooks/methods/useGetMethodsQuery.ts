import {
  keepPreviousData,
  queryOptions,
  useSuspenseQuery,
} from '@tanstack/react-query'

import { getMethods } from '../../requests'

export const getMethodsQueryOptions = (
  settings?: QuerySettings<typeof getMethods>,
) =>
  queryOptions({
    queryKey: ['methods'],
    queryFn: ({ signal }) =>
      getMethods({
        config: {
          signal,
          ...settings?.config,
        },
      }),
  })

export const useGetMethodsQuery = (
  settings?: QuerySettings<typeof getMethods>,
) =>
  useSuspenseQuery({
    placeholderData: keepPreviousData,
    ...getMethodsQueryOptions(settings),
    ...settings?.options,
  })
