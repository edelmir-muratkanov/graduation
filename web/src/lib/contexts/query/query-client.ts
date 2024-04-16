import { flushSync } from 'react-dom'
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
        if (
          error.config?.url?.includes('auth') ||
          [400, 403, 404].includes(error.response?.status || 0)
        ) {
          return false
        }

        return count <= 3
      },
    },
  },
  queryCache: new QueryCache({
    onError: ({ response }, query) => {
      if (response?.status === 401) {
        postRefresh({})
          .then(({ data }) => {
            flushSync(() => {
              localStorage.setItem(STORAGE_KEYS.AccessToken, data.token)
            })
            return query.fetch()
          })
          .catch(() => {
            flushSync(() => {
              localStorage.removeItem(STORAGE_KEYS.AccessToken)
            })
          })
      }

      toast.error(response?.data.message ?? DEFAULT_ERROR, {
        cancel: { label: 'Закрыть' },
      })
    },
  }),
  mutationCache: new MutationCache({
    onError: ({ response }, vars, _, mutation) => {
      if (response?.status === 401) {
        postRefresh({})
          .then(({ data }) => {
            flushSync(() => {
              localStorage.setItem(STORAGE_KEYS.AccessToken, data.token)
            })
            return mutation.execute(vars)
          })
          .catch(() => {
            flushSync(() => {
              localStorage.removeItem(STORAGE_KEYS.AccessToken)
            })
          })
        toast.error(response?.data.message ?? DEFAULT_ERROR, {
          cancel: { label: 'Закрыть' },
        })
      }
    },
  }),
})
