import { createFileRoute, redirect } from '@tanstack/react-router'

export const Route = createFileRoute('/_user')({
  loader: ({ context, location }) => {
    if (!context.user) {
      throw redirect({
        to: '/auth',
        search: {
          stage: 'login',
          redirectUrl: location.href,
        },
      })
    }
  },
})
