import { createFileRoute, redirect } from '@tanstack/react-router'
import { z } from 'zod'

import { AuthLoading } from '@/pages/auth/loading'

const authSearchSchema = z.object({
  stage: z
    .enum(['login', 'register'] as const)
    .default('login')
    .optional(),
  redirectUrl: z.string().optional().default('/'),
})

export const Route = createFileRoute('/auth/')({
  validateSearch: authSearchSchema,
  beforeLoad: ({ context, search }) => {
    if (context.user?.id) {
      throw redirect({ to: search.redirectUrl })
    }
  },

  pendingComponent: AuthLoading,
})
