import { useState } from 'react'
import { useFieldArray, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import {
  useGetPropertiesQuery,
  usePostAddProjectParametersMutation,
} from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import { useProjectPage } from '../../useProjectPage'

import {
  type AddProjectParametersFormSchema,
  addProrjectParametersFormSchema,
} from './constants'

export const useAddProjectParameters = () => {
  const { state } = useProjectPage()
  const [isOpen, setIsOpen] = useState(false)

  const getPropertiesQuery = useGetPropertiesQuery({
    config: { params: { pageSize: 100 } },
  })

  const form = useForm<AddProjectParametersFormSchema>({
    resolver: zodResolver(addProrjectParametersFormSchema),
    defaultValues: {
      data: [],
    },
  })

  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: 'data',
  })

  const addProjectParametersMutation = usePostAddProjectParametersMutation(
    state.project.id,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Параметр успешно добавлен.')

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    addProjectParametersMutation.mutateAsync({
      params: values.data,
    }),
  )

  const addParameter = () => append({ propertyId: '', value: 0 })
  const removeParameter = (index: number) => remove(index)

  const filteredProperties = getPropertiesQuery.data.data.items.filter(
    i => !state.project.parameters.some(p => p.name === i.name),
  )

  return {
    state: {
      form,
      isOpen,
      fields,
      properties: filteredProperties,
      loading: addProjectParametersMutation.isPending,
    },
    functions: {
      onSubmit,
      setIsOpen,
      addParameter,
      removeParameter,
    },
  }
}
