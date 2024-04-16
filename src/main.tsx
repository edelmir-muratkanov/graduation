import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { getProfileQueryOptions } from './lib/api'
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
    await queryClient
      .fetchQuery(getProfileQueryOptions())
      .catch(() => {})
      .then(data => {
        if (data) {
          providerProps.profile.defaultUser = data.data
        }
      })
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
