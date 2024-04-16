import type { FC, ReactNode } from 'react'

import type { ProfileProviderProps, QueryProviderProps } from './lib/contexts'
import { ProfileProvider, QueryProvider } from './lib/contexts'

export interface ProvidersProps {
  children: ReactNode
  query: Omit<QueryProviderProps, 'children'>
  profile: Omit<ProfileProviderProps, 'children'>
}

export const Providers: FC<ProvidersProps> = ({ profile, children, query }) => (
  <ProfileProvider {...profile}>
    <QueryProvider {...query}>{children}</QueryProvider>
  </ProfileProvider>
)
