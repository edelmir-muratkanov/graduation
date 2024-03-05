import type { FC, ReactNode } from 'react'

import type { AuthProviderProps, QueryProviderProps } from './lib/contexts'
import { AuthProvider, QueryProvider } from './lib/contexts'

export interface ProvidersProps {
  children: ReactNode
  query: Omit<QueryProviderProps, 'children'>
  auth: Omit<AuthProviderProps, 'children'>
}

export const Providers: FC<ProvidersProps> = ({ auth, children, query }) => (
  <AuthProvider {...auth}>
    <QueryProvider {...query}>{children}</QueryProvider>
  </AuthProvider>
)
