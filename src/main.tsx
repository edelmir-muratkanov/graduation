import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { MutationCache, QueryCache, QueryClient } from '@tanstack/react-query'
import { createRouter, RouterProvider } from '@tanstack/react-router'
import type { AxiosError } from 'axios'
import { toast } from 'sonner'

import type { ProvidersProps } from './providers'
import { Providers } from './providers'
import { routeTree } from './routeTree.gen'

import '@/assets/index.css'

const rootElement = document.getElementById('root')!
const root = createRoot(rootElement)
const router = createRouter({ routeTree })
const DEFAULT_ERROR = 'Something went wrong'

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}

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

const init = () => {
  const providerProps: Omit<ProvidersProps, 'children'> = {
    auth: {
      defaultTokens: undefined,
      defaultUser: undefined,
    },
    query: {
      client: queryClient,
    },
  }

  root.render(
    <StrictMode>
      <Providers {...providerProps}>
        <RouterProvider router={router} />
      </Providers>
    </StrictMode>,
  )
}

init()
