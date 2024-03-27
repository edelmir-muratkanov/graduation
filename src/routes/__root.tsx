import { lazy, Suspense } from 'react'
import type { QueryClient } from '@tanstack/react-query'
import { createRootRouteWithContext, Outlet } from '@tanstack/react-router'

import { ErrorComponent } from '@/components/error-component'
import { Footer } from '@/components/footer'
import { Header } from '@/components/header'
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
    <div className='relative flex min-h-screen flex-col bg-background'>
      <Header />
      <main className='flex flex-1 container pt-[1.5rem]'>
        <Outlet />
      </main>
      <Footer />
      <Toaster duration={TOASTER_DURATION} position='top-center' />
      <Suspense fallback={null}>
        <TanStackRouterDevtools />
        <ReactQueryDevtools />
      </Suspense>
    </div>
  ),

  notFoundComponent: () => <NotFound />,
  errorComponent: () => <ErrorComponent />,
})
