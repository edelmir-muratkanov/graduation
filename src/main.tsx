import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { MutationCache, QueryCache, QueryClient } from '@tanstack/react-query'
import type { AxiosError } from 'axios'
import { toast } from 'sonner'

import { getProfile } from './lib/api'
import { STORAGE_KEYS } from './lib/constants'
import { App } from './app'
import type { ProvidersProps } from './providers'
import { Providers } from './providers'

import '@/assets/index.css'

const rootElement = document.getElementById('root')!
const root = createRoot(rootElement)
const DEFAULT_ERROR = 'Something went wrong'

const queryClient = new QueryClient({
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
