import { createFileRoute } from '@tanstack/react-router'

import { getMethodsQueryOptions, getPropertiesQueryOptions } from '@/lib/api'
import { CreateProjectLoading } from '@/pages/create-project/loading'

export const Route = createFileRoute('/_user/projects/new/')({
  loader: ({ context }) => {
    context.queryClient.ensureQueryData(getMethodsQueryOptions())
    context.queryClient.ensureQueryData(getPropertiesQueryOptions())
  },

  pendingComponent: CreateProjectLoading,
})
