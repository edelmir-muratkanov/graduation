import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { getProfile } from './lib/api'
import { STORAGE_KEYS } from './lib/constants'
import { queryClient } from './lib/contexts'
import { App } from './app'
import type { ProvidersProps } from './providers'
import { Providers } from './providers'

import '@/assets/index.css'

const rootElement = document.getElementById('root')!
const root = createRoot(rootElement)

const init = async () => {
  const token = localStorage.getItem(STORAGE_KEYS.AccessToken)
  const providerProps: Omit<ProvidersProps, 'children'> = {
    profile: {
      defaultUser: undefined,
    },
    query: {
      client: queryClient,
    },
  }

  if (token) {
    const getProfileQuery = await queryClient.fetchQuery({
      queryKey: ['getProfile'],
      queryFn: () => getProfile(),
    })

    providerProps.profile.defaultUser = getProfileQuery.data
  }

  root.render(
    <StrictMode>
      <Providers {...providerProps}>
        <App />
      </Providers>
    </StrictMode>,
  )
}

init()
