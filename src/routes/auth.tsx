import { createFileRoute } from '@tanstack/react-router'
import { z } from 'zod'

import { AuthLoading } from '@/pages/auth/loading'

const authSearchSchema = z.object({
  stage: z.enum(['login', 'register'] as const).catch('login'),
  redirectUrl: z.string().catch('/'),
})

export const Route = createFileRoute('/auth')({
  validateSearch: authSearchSchema,
  beforeLoad: () => {},
  pendingComponent: () => <AuthLoading />,
})
