import type { QueryClient } from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router'
import { TanStackRouterDevtools } from '@tanstack/router-devtools'

import { Toaster } from '@/components/ui'

const TOASTER_DURATION = 5000

export const Route = createRootRouteWithContext<{
  user?: User
  queryClient: QueryClient
}>()({
  component: () => (
    <>
      <div className='flex container h-screen p-[2rem]'>
        <Outlet />
      </div>
      <Toaster duration={TOASTER_DURATION} position='top-right' />
      <TanStackRouterDevtools />
      <ReactQueryDevtools />
    </>
  ),
})
