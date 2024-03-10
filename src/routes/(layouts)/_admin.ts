import { createFileRoute, notFound } from '@tanstack/react-router'

export const Route = createFileRoute('/(layouts)/_admin')({
  beforeLoad: ({ context }) => {
    if (context.user?.role !== 'Admin') {
      throw notFound()
    }
  },
})
