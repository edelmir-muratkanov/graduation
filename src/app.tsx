import {
  createRouteMask,
  createRouter,
  RouterProvider,
} from '@tanstack/react-router'

import { queryClient, useProfile } from './lib/contexts'
import { routeTree } from './routeTree.gen'

const NoSearchProject = createRouteMask({
  routeTree,
  from: '/projects/$projectId',
  to: '/projects/$projectId',
  params: prev => ({ projectId: prev.projectId }),
})

const router = createRouter({
  routeTree,
  context: {
    user: undefined,
    queryClient,
  },
  routeMasks: [NoSearchProject],
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
