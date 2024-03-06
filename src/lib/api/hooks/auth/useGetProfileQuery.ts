import { useQuery } from '@tanstack/react-query'

import { getProfile } from '../../requests'

export const useGetProfileQuery = (
  settings?: QuerySettings<typeof getProfile>,
) =>
  useQuery({
    queryKey: ['getProfile'],
    queryFn: () => getProfile({ config: settings?.config }),
    ...settings?.options,
  })
