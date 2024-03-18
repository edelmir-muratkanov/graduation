import type { ChangeEvent } from 'react'
import { useFieldArray, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import type { Option } from '@/components/ui'
import { useGetPropertiesQuery, usePostCreateMethodMutation } from '@/lib/api'

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
      data: [],
    },
  })
  const dataFormArray = useFieldArray({
    control: form.control,
    name: 'data',
  })

  const postCreateMethodMutation = usePostCreateMethodMutation({
    options: {
      onSuccess() {
        toast.success('Method successfully created', {
          cancel: { label: 'Close' },
        })
      },
    },
  })

  const onSubmit = form.handleSubmit(async values => {
    await postCreateMethodMutation.mutateAsync({
      params: {
        name: values.name,
        collectorTypes: values.collectorTypes,
        data: values.data.map(d => ({
          propertyId: d.propertyId,
          parameters: {
            first: d.parameters.first
              ? {
                  x: +d.parameters.first.x,
                  xMax: +d.parameters.first.xMax,
                  xMin: +d.parameters.first.xMin,
                }
              : undefined,
            second: d.parameters.second
              ? {
                  x: +d.parameters.second.x,
                  xMax: +d.parameters.second.xMax,
                  xMin: +d.parameters.second.xMin,
                }
              : undefined,
          },
        })),
      },
    })
  })

  form.watch('data')

  const getPropertiesQuery = useGetPropertiesQuery()

  const getMultipleSelectorValue = (value: CollectorType): Option => ({
    label: value === 'Carbonate' ? 'Carbonate' : 'Terrigen',
    value: value === 'Carbonate' ? 'Carbonate' : 'Terrigen',
  })

  const setParameter = (index: number, type: 'first' | 'second') => {
    form.setValue(`data.${index}.parameters.${type}`, {
      x: '',
      xMax: '',
      xMin: '',
    })
  }

  const addData = () =>
    dataFormArray.append({
      propertyId: '',
      parameters: {},
    })

  const removeData = (index: number) => dataFormArray.remove(index)

  const convertToNumber = (e: ChangeEvent<HTMLInputElement>) => {
    const n = parseFloat(e.target.value)
    return Number.isNaN(n) && !Number.isFinite(n) ? '' : n
  }

  return {
    state: {
      form,
      formErrors: form.formState.errors,
      loading: false,
      dataFields: dataFormArray.fields,
      properties: getPropertiesQuery.data.data.items,
    },
    functions: {
      onSubmit,
      getMultipleSelectorValue,
      setParameter,
      addData,
      removeData,
      convertToNumber,
    },
  }
}
