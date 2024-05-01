import type { ChangeEvent } from 'react'
import { useFieldArray, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { useGetPropertiesQuery, usePostCreateMethodMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import {
  type CreateMethodFormSchema,
  createMethodFormSchema,
} from '../constants'

export const useCreateMethodForm = () => {
  const form = useForm<CreateMethodFormSchema>({
    resolver: zodResolver(createMethodFormSchema),
    defaultValues: {
      name: '',
      collectorTypes: [],
      parameters: [],
    },
  })
  const parametersFormArray = useFieldArray({
    control: form.control,
    name: 'parameters',
  })

  const postCreateMethodMutation = usePostCreateMethodMutation({
    options: {
      onSuccess() {
        queryClient.invalidateQueries({ queryKey: ['methods'] })
        toast.success('Метод успешно создан.', {
          cancel: { label: 'Закрыть' },
        })

        form.reset()
      },
    },
  })

  const onSubmit = form.handleSubmit(async values => {
    await postCreateMethodMutation.mutateAsync({
      params: {
        name: values.name,
        collectorTypes: values.collectorTypes,
        parameters: values.parameters.map(d => ({
          propertyId: d.propertyId,
          firstParameters: d.first
            ? {
                avg: +d.first.avg,
                max: +d.first.max,
                min: +d.first.min,
              }
            : undefined,
          secondParameters: d.second
            ? {
                avg: +d.second.avg,
                max: +d.second.max,
                min: +d.second.min,
              }
            : undefined,
        })),
      },
    })
  })

  const parameters = form.watch('parameters')

  const getPropertiesQuery = useGetPropertiesQuery({
    config: { params: { pageSize: 100, sortColumn: 'name' } },
  })

  const setParameter = (index: number, type: 'first' | 'second') => {
    form.setValue(`parameters.${index}.${type}`, {
      avg: '',
      max: '',
      min: '',
    })
  }

  const addData = () =>
    parametersFormArray.append({
      propertyId: '',
    })

  const removeData = (index: number) => parametersFormArray.remove(index)

  const convertToNumber = (e: ChangeEvent<HTMLInputElement>) => {
    const n = parseFloat(e.target.value)
    return Number.isNaN(n) && !Number.isFinite(n) ? '' : n
  }

  return {
    state: {
      form,
      formErrors: form.formState.errors,
      loading: false,
      parametersFields: parametersFormArray.fields,
      properties: getPropertiesQuery.data.data.items,
      parameters,
    },
    functions: {
      onSubmit,
      setParameter,
      addData,
      removeData,
      convertToNumber,
    },
  }
}
