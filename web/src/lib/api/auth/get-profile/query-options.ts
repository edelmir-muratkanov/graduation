import { queryOptions } from '@tanstack/react-query'

import { getProfile } from './request'

export const getProfileQueryOptions = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  queryOptions({
    queryKey: ['getProfile'],
    queryFn: () => getProfile({ config: settings?.config }),
  })
