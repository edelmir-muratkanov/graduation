import { useQuery } from '@tanstack/react-query'

import { getProfileQueryOptions } from './query-options'
import type { getProfile } from './request'

export const useGetProfileQuery = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  useQuery({
    ...getProfileQueryOptions(),
    ...settings?.options,
  })
