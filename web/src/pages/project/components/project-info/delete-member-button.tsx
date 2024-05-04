import { X } from 'lucide-react'
import { toast } from 'sonner'

import {
  Text,
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui'
import { useDeleteProjectMemberMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

interface DeleteMemberButtonProps {
  projectId: string
  memberId: string
}

export const DeleteMemberButton = ({
  projectId,
  memberId,
}: DeleteMemberButtonProps) => {
  const deleteProjectMemberMutation = useDeleteProjectMemberMutation(
    projectId,
    memberId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Участник успешно удален.')
        },
      },
    },
  )

  const onClick = async () => deleteProjectMemberMutation.mutateAsync({})

  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger onClick={onClick}>
          <X className='size-4' />
        </TooltipTrigger>
        <TooltipContent>
          <Text>Удалить участника</Text>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
