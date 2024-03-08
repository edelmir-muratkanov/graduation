import type { FC, ReactNode } from 'react'
import {
  MutationCache,
  QueryCache,
  QueryClient,
  QueryClientProvider,
} from '@tanstack/react-query'
import type { AxiosError } from 'axios'
import { toast } from 'sonner'

import { DEFAULT_ERROR } from '@/lib/constants'

export interface QueryProviderProps {
  children: ReactNode
}

export const queryClient = new QueryClient({
  defaultOptions: { queries: { refetchOnWindowFocus: false } },
  queryCache: new QueryCache({
    onError: cause => {
      const { response } = cause as AxiosError<BaseResponse>
      toast.error(response?.data.message ?? DEFAULT_ERROR, {
        cancel: { label: 'Close' },
      })
    },
  }),
  mutationCache: new MutationCache({
    onError: cause => {
      const { response } = cause as AxiosError<BaseResponse>
      toast.error(response?.data.message ?? DEFAULT_ERROR, {
        cancel: { label: 'Close' },
      })
    },
  }),
})

export const QueryProvider: FC<QueryProviderProps> = ({ children }) => (
  <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
)
