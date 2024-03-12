import { useFieldArray, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import {
  useGetMethodsQuery,
  useGetPropertiesQuery,
  usePostCreateProjectMutation,
} from '@/lib/api'

import type { CreateProjectFormSchema } from './constants'
import { createProjectFormSchema } from './constants'

export const useCreateProjectForm = () => {
  const form = useForm<CreateProjectFormSchema>({
    resolver: zodResolver(createProjectFormSchema),

    defaultValues: {
      name: '',
      country: '',
      operator: '',
      parameters: [{ propertyId: '', value: 0 }],
    },
  })

  const parametersFormArray = useFieldArray({
    control: form.control,
    name: 'parameters',
  })
  const createProjectMutation = usePostCreateProjectMutation({
    options: {
      onSuccess() {
        toast.success('Project created sucessfull')
      },
    },
  })

  const getPropertiesQuery = useGetPropertiesQuery()
  const getMethodsQuery = useGetMethodsQuery()

  const onSumbit = form.handleSubmit(async values => {
    await createProjectMutation.mutateAsync({
      params: values,
    })
  })

  return {
    state: {
      form,
      loading: createProjectMutation.isPending,
      parametersFormArray,
      properties: getPropertiesQuery.data.data.items,
      methods: getMethodsQuery.data.data.items,
    },
    functions: { onSumbit },
  }
}
