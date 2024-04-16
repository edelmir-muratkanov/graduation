import { createRouter, RouterProvider } from '@tanstack/react-router'
import type { AxiosError } from 'axios'

import { queryClient, useProfile } from './lib/contexts'
import { routeTree } from './routeTree.gen'

const router = createRouter({
  routeTree,
  context: {
    user: undefined,
    queryClient,
  },
})

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}

declare module '@tanstack/react-query' {
  interface Register {
    defaultError: AxiosError<BaseErrorResponse>
  }
}

export const App = () => {
  const { user } = useProfile()

  return <RouterProvider router={router} context={{ user }} />
}
