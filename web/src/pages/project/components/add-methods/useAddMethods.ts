import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { useGetMethodsQuery, usePostAddProjectMethodsMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import { useProjectPage } from '../../useProjectPage'

import type { AddMethodsFormSchema } from './constants'
import { addMethodsFormSchema } from './constants'

export const useAddMethods = () => {
  const [isOpen, setIsOpen] = useState(false)
  const methodsQuery = useGetMethodsQuery({
    config: {
      params: {
        pageSize: 100,
        sortColumn: 'name',
      },
    },
  })

  const {
    state: { project },
  } = useProjectPage()

  const form = useForm<AddMethodsFormSchema>({
    resolver: zodResolver(addMethodsFormSchema),
    defaultValues: {
      data: [],
    },
  })

  const addProjectMethodsMutation = usePostAddProjectMethodsMutation(
    project.id,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Метод успешно добавлен.')

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    addProjectMethodsMutation.mutateAsync({
      params: values.data,
    }),
  )

  const filteredMethods = methodsQuery.data.data.items.filter(
    m => !project.methods.some(pm => pm.id === m.id),
  )

  return {
    state: {
      form,
      isOpen,
      methods: filteredMethods,
    },
    functions: {
      setIsOpen,
      onSubmit,
    },
  }
}
