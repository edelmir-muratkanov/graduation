import type { FC, ReactNode } from 'react'
import { QueryClientProvider } from '@tanstack/react-query'

import { queryClient } from './query-client'

export interface QueryProviderProps {
  children: ReactNode
}

export const QueryProvider: FC<QueryProviderProps> = ({ children }) => (
  <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
)
