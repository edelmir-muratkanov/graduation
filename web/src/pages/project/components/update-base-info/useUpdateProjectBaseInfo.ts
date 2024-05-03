import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { usePatchUpdateProjectBaseMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import { useProjectPage } from '../../useProjectPage'

import type { UpdateProjectBaseFormSchema } from './constants'
import { updateProjectBaseFormSchema } from './constants'

export const useUpdateBaseInfo = () => {
  const [isOpen, setIsOpen] = useState(false)

  const { state } = useProjectPage()
  const form = useForm<UpdateProjectBaseFormSchema>({
    resolver: zodResolver(updateProjectBaseFormSchema),
    defaultValues: {
      name: null,
      country: null,
      operator: null,
      collectorType: null,
      type: null,
    },
  })

  const patchUpdateProjectBaseMutation = usePatchUpdateProjectBaseMutation(
    state.project.id,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Проект успешно обновлен.', {
            cancel: { label: 'Закрыть' },
          })

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    patchUpdateProjectBaseMutation.mutateAsync({
      params: values,
    }),
  )

  return {
    state: { form, isOpen, loading: patchUpdateProjectBaseMutation.isPending },
    functions: { onSubmit, setIsOpen },
  }
}
