import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { usePatchUpdateMethodBaseMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import { useMethodPage } from '../../useMethodPage'

import {
  type UpdateMethodBaseFormSchema,
  updateMethodBaseFormSchema,
} from './constants'

export const useUpdateBaseInfo = () => {
  const [isOpen, setIsOpen] = useState(false)

  const { methodId } = useMethodPage()
  const form = useForm<UpdateMethodBaseFormSchema>({
    resolver: zodResolver(updateMethodBaseFormSchema),
    defaultValues: {
      name: null,
      collectorTypes: null,
    },
  })

  const patchUpdateMethodBaseMutation = usePatchUpdateMethodBaseMutation(
    methodId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['methods'] })
          toast.success('Метод успешно обновлен.', {
            cancel: { label: 'Закрыть' },
          })

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    patchUpdateMethodBaseMutation.mutateAsync({
      params: {
        name: values.name,
        collectorTypes: values.collectorTypes,
      },
    }),
  )

  return {
    state: { form, isOpen },
    functions: { onSubmit, setIsOpen },
  }
}
