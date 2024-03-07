import { flushSync } from 'react-dom'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useNavigate } from '@tanstack/react-router'
import { toast } from 'sonner'
import type { z } from 'zod'

import { usePostRegisterMutation } from '@/lib/api'
import { STORAGE_KEYS } from '@/lib/constants'
import { useProfile } from '@/lib/contexts'
import { Route } from '@/routes/auth'

import { registerFormSchema } from './constants'

export const useRegisterForm = () => {
  const { redirectUrl } = Route.useSearch()
  const { setUser } = useProfile()
  const navigate = useNavigate()

  const postRegisterMutation = usePostRegisterMutation({
    options: {
      onSuccess: () => {
        toast.success('Your account has been created 👍', {
          cancel: { label: 'Close' },
          description: 'We are very glad to see you, have fun',
        })
      },
    },
  })

  const form = useForm<z.infer<typeof registerFormSchema>>({
    resolver: zodResolver(registerFormSchema),
    defaultValues: {
      email: '',
      password: '',
      confirmPassword: '',
    },
  })

  const onSubmit = form.handleSubmit(async values => {
    const res = await postRegisterMutation.mutateAsync({
      params: values,
    })

    if (res.data.token && res.data.user) {
      flushSync(() => {
        localStorage.setItem(STORAGE_KEYS.AccessToken, res.data.token)
        setUser(res.data.user)
      })
      navigate({ to: redirectUrl, replace: true })
    }
  })

  const goToLogin = () =>
    navigate({ to: '/auth', search: { redirectUrl, stage: 'login' } })

  return {
    loading: postRegisterMutation.isPending,
    form,
    onSubmit,
    goToLogin,
  }
}
