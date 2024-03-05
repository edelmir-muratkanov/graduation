import type { ComponentProps, FC, ReactNode } from 'react'
import { QueryClientProvider } from '@tanstack/react-query'

export interface QueryProviderProps
  extends ComponentProps<typeof QueryClientProvider> {
  children: ReactNode
}

export const QueryProvider: FC<QueryProviderProps> = ({ children, client }) => (
  <QueryClientProvider client={client}>{children}</QueryClientProvider>
)
