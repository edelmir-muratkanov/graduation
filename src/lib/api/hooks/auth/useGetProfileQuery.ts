import { useQuery } from '@tanstack/react-query'

import { getProfile } from '../..'

export const useGetProfileQuery = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  useQuery({
    queryKey: ['getProfile'],
    queryFn: () => getProfile({ config: settings?.config }),
    ...settings?.options,
  })
