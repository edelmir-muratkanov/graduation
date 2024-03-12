import { queryOptions, useQuery } from '@tanstack/react-query'

import { getProfile } from '../../requests'

export const getProfileQueryOptions = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  queryOptions({
    queryKey: ['getProfile'],
    queryFn: () => getProfile({ config: settings?.config }),
  })

export const useGetProfileQuery = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  useQuery({
    ...getProfileQueryOptions(),
    ...settings?.options,
  })
