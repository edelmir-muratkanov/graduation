import { flushSync } from 'react-dom'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { getRouteApi, useNavigate } from '@tanstack/react-router'
import { toast } from 'sonner'
import type { z } from 'zod'

import { usePostRegisterMutation } from '@/lib/api'
import { STORAGE_KEYS } from '@/lib/constants'
import { useProfile } from '@/lib/contexts'

import { registerFormSchema } from './constants'

const route = getRouteApi('/auth/')

export const useRegisterForm = () => {
  const { redirectUrl } = route.useSearch()
  const { setUser } = useProfile()
  const navigate = useNavigate()

  const postRegisterMutation = usePostRegisterMutation({
    options: {
      onSuccess: () => {
        toast.success('Ваша учетная запись была создана. 👍', {
          cancel: { label: 'Закрыть' },
          description: 'Мы очень рады видеть вас!',
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
