import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router'
import { TanStackRouterDevtools } from '@tanstack/router-devtools'

import { Toaster } from '@/components/ui'

const TOASTER_DURATION = 5000

export const Route = createRootRouteWithContext<{ user?: User }>()({
  component: () => (
    <>
      <div className='flex h-screen items-center justify-center p-4'>
        <Outlet />
      </div>
      <Toaster duration={TOASTER_DURATION} position='top-right' />
      <TanStackRouterDevtools />
      <ReactQueryDevtools />
    </>
  ),
})
