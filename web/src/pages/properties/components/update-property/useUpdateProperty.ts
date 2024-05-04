import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { usePatchUpdatePropertyMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import type { UpdatePropertyFormSchema } from './constants'
import { updatePropertyFormSchema } from './constants'

export const useUpdateProperty = (propertyId: string) => {
  const [isOpen, setIsOpen] = useState(false)

  const form = useForm<UpdatePropertyFormSchema>({
    resolver: zodResolver(updatePropertyFormSchema),
    defaultValues: {
      name: null,
      unit: null,
    },
  })

  const patchUpdatePropertyMutation = usePatchUpdatePropertyMutation(
    propertyId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['properties'] })
          toast.success('Свойство успешно обновлено.', {
            cancel: { label: 'Закрыть' },
          })

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    patchUpdatePropertyMutation.mutateAsync({
      params: values,
    }),
  )

  return {
    state: { form, isOpen },
    functions: { onSubmit, setIsOpen },
  }
}
