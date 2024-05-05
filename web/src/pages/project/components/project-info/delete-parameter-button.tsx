import { X } from 'lucide-react'
import { toast } from 'sonner'

import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import { Text } from '@/components/ui/typography'
import { useDeleteProjectParameterMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

interface DeleteParameterButtonProps {
  projectId: string
  parameterId: string
}

export const DeleteParameterButton = ({
  projectId,
  parameterId,
}: DeleteParameterButtonProps) => {
  const deleteProjectParameterMutation = useDeleteProjectParameterMutation(
    projectId,
    parameterId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Параметр успешно удален.')
        },
      },
    },
  )

  const onClick = async () => deleteProjectParameterMutation.mutateAsync({})

  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger onClick={onClick}>
          <X className='size-4' />
        </TooltipTrigger>
        <TooltipContent>
          <Text>Удалить параметр</Text>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
