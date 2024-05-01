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
        toast.success('Вход успешно выполнен 👍', {
          cancel: { label: 'Закрыть' },
          description: 'Мы очень рады видеть вас!',
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
    const {
      data: { token, ...user },
    } = await postLoginMutation.mutateAsync({
      params: values,
    })

    if (token && user.id) {
      flushSync(() => {
        localStorage.setItem(STORAGE_KEYS.AccessToken, token)
        setUser(user)
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
