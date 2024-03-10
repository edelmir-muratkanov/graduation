import { createRouter, RouterProvider } from '@tanstack/react-router'

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
export const App = () => {
  const { user } = useProfile()

  return <RouterProvider router={router} context={{ user }} />
}
