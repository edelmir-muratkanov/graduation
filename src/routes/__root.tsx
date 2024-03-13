import { lazy, Suspense } from 'react'
import type { QueryClient } from '@tanstack/react-query'
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router'

import { ErrorComponent } from '@/components/error-component'
import { NotFound } from '@/components/not-found'
import { Toaster } from '@/components/ui'

const TOASTER_DURATION = 5000

const isProd = process.env.NODE_ENV === 'production'
const TanStackRouterDevtools = isProd
  ? () => null
  : lazy(() =>
      import('@tanstack/router-devtools').then(d => ({
        default: d.TanStackRouterDevtools,
      })),
    )

const ReactQueryDevtools = isProd
  ? () => null
  : lazy(() =>
      import('@tanstack/react-query-devtools').then(d => ({
        default: d.ReactQueryDevtools,
      })),
    )

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
      <Suspense fallback={null}>
        <TanStackRouterDevtools />
        <ReactQueryDevtools />
      </Suspense>
    </>
  ),

  notFoundComponent: () => <NotFound />,
  errorComponent: () => <ErrorComponent />,
})
