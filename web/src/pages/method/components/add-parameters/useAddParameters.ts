import { useState } from 'react'
import { useFieldArray, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { useGetPropertiesQuery } from '@/lib/api'
import { usePostAddMethodParametersMutation } from '@/lib/api/methods/post-add-parameters'
import { queryClient } from '@/lib/contexts'

import { useMethodPage } from '../../useMethodPage'

import type { AddParametersFormSchema } from './constants'
import { addParametersFormSchema } from './constants'

export const useAddParameter = () => {
  const { methodId, methodData } = useMethodPage()
  const [isOpen, setIsOpen] = useState(false)

  const getPropertiesQuery = useGetPropertiesQuery({
    config: { params: { pageSize: 100, sortColumn: 'name' } },
  })

  const form = useForm<AddParametersFormSchema>({
    resolver: zodResolver(addParametersFormSchema),
    defaultValues: {
      data: [],
    },
  })

  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: 'data',
  })

  const postAddParametersMutation = usePostAddMethodParametersMutation(
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
    postAddParametersMutation.mutateAsync({
      params: values.data,
    }),
  )

  const addParameter = () =>
    append({ propertyId: '', first: null, second: null })

  const removeParameter = (index: number) => remove(index)

  const setParameter = (index: number, type: 'first' | 'second') => {
    form.setValue(`data.${index}.${type}`, {
      avg: 0,
      max: 0,
      min: 0,
    })
  }

  return {
    state: {
      form,
      formErrors: form.formState.errors,
      fields,
      properties: getPropertiesQuery.data.data.items.filter(
        i => !methodData.data.parameters.some(p => p.propertyName === i.name),
      ),
      loading: postAddParametersMutation.isPending,
      isOpen,
    },
    functions: {
      onSubmit,
      addParameter,
      removeParameter,
      setParameter,
      setIsOpen,
    },
  }
}
