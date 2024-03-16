import { MutationCache, QueryCache, QueryClient } from '@tanstack/react-query'
import { toast } from 'sonner'

import { postRefresh } from '@/lib/api'
import { DEFAULT_ERROR, STORAGE_KEYS } from '@/lib/constants'

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      staleTime: 30_000,
      retry: (count, error) => {
        if ([400, 401, 403, 404].includes(error.response?.status || 0)) {
          return false
        }

        return count <= 1
      },
    },
  },
  queryCache: new QueryCache({
    onError: ({ response }) => {
      if (response?.status === 401) {
        postRefresh({})
          .then(({ data }) => {
            localStorage.setItem(STORAGE_KEYS.AccessToken, data.token)
          })
          .catch(() => {})

        return
      }

      toast.error(response?.data.message ?? DEFAULT_ERROR, {
        cancel: { label: 'Close' },
      })
    },
  }),
  mutationCache: new MutationCache({
    onError: ({ response }) => {
      if (response?.status === 401) {
        postRefresh({})
          .then(({ data }) => {
            localStorage.setItem(STORAGE_KEYS.AccessToken, data.token)
          })
          .catch(() => {})

        return
      }

      toast.error(response?.data.message ?? DEFAULT_ERROR, {
        cancel: { label: 'Close' },
      })
    },
  }),
})
