import { useSuspenseQuery } from '@tanstack/react-query'

import { getMethodQueryOptions } from './query-options'
import type { getMethod } from './request'

export const useGetMethodQuery = (
  methodId: string,
  settings?: QuerySettings<typeof getMethod>,
) =>
  useSuspenseQuery({
    ...getMethodQueryOptions(methodId, settings),
    ...settings?.options,
  })
