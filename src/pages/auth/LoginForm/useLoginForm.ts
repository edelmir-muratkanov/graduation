import { flushSync } from 'react-dom'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { getRouteApi, useNavigate } from '@tanstack/react-router'
import { toast } from 'sonner'
import type { z } from 'zod'

import { usePostLoginMutation } from '@/lib/api'
import { STORAGE_KEYS } from '@/lib/constants'
import { useProfile } from '@/lib/contexts'

import { loginFormSchema } from './constants'

const route = getRouteApi('/auth/')

export const useLoginForm = () => {
  const { redirectUrl } = route.useSearch()
  const { setUser } = useProfile()
  const navigate = useNavigate()

  const postLoginMutation = usePostLoginMutation({
    options: {
      onSuccess: () => {
        toast.success('Sign in is successful 👍', {
          cancel: { label: 'Close' },
          description: 'We are very glad to see you, have fun',
        })
      },
    },
  })

  const form = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
    defaultValues: {
      email: '',
      password: '',
    },
  })

  const onSubmit = form.handleSubmit(async values => {
    const res = await postLoginMutation.mutateAsync({
      params: values,
    })

    if (res.data.token && res.data.user) {
      flushSync(() => {
        localStorage.setItem(STORAGE_KEYS.AccessToken, res.data.token)
        setUser(res.data.user)
      })
      navigate({ to: redirectUrl ?? '/', replace: true })
    }
  })

  const goToRegister = () =>
    navigate({ to: '/auth', search: { redirectUrl, stage: 'register' } })

  return {
    loading: postLoginMutation.isPending,
    form,
    onSubmit,
    goToRegister,
  }
}
