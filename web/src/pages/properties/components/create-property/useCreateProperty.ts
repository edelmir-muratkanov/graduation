import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { usePostCreatePropertyMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import type { CreatePropertyFormSchema } from './constants'
import { createPropertyFormSchema } from './constants'

export const useCreateProperty = () => {
  const [isOpen, setIsOpen] = useState(false)

  const form = useForm<CreatePropertyFormSchema>({
    resolver: zodResolver(createPropertyFormSchema),
    defaultValues: {
      name: '',
      unit: '',
    },
  })

  const postCreatePropertyMutation = usePostCreatePropertyMutation({
    options: {
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: ['properties'] })
        toast.success('Метод успешно обновлен.', {
          cancel: { label: 'Закрыть' },
        })

        form.reset()
        setIsOpen(false)
      },
    },
  })

  const onSubmit = form.handleSubmit(async values =>
    postCreatePropertyMutation.mutateAsync({
      params: values,
    }),
  )

  return {
    state: { form, isOpen },
    functions: { onSubmit, setIsOpen },
  }
}
