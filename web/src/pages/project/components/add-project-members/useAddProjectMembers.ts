import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'

import { useGetUsersQuery, usePostAddProjectMembersMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

import { useProjectPage } from '../../useProjectPage'

import type { AddMembersFormSchema } from './constants'
import { addMembersFormSchema } from './constants'

export const useAddProjectMembers = () => {
  const [isOpen, setIsOpen] = useState(false)
  const usersQuery = useGetUsersQuery({
    config: {
      params: {
        pageSize: 100,
      },
    },
  })

  const {
    state: { project },
  } = useProjectPage()

  const form = useForm<AddMembersFormSchema>({
    resolver: zodResolver(addMembersFormSchema),
    defaultValues: {
      data: [],
    },
  })

  const addProjectMembersMutation = usePostAddProjectMembersMutation(
    project.id,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Участник успешно добавлен.')

          form.reset()
          setIsOpen(false)
        },
      },
    },
  )

  const onSubmit = form.handleSubmit(async values =>
    addProjectMembersMutation.mutateAsync({
      params: values.data,
    }),
  )

  const filteredUsers = usersQuery.data.data.items.filter(
    u =>
      !project.members.some(pm => pm.id === u.id) && project.ownerId !== u.id,
  )

  return {
    state: {
      form,
      isOpen,
      users: filteredUsers,
    },
    functions: {
      setIsOpen,
      onSubmit,
    },
  }
}
