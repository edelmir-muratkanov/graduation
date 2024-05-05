import { X } from 'lucide-react'
import { toast } from 'sonner'

import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import { Text } from '@/components/ui/typography'
import { useDeleteProjectMethodMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

interface DeleteMethodButtonProps {
  projectId: string
  methodId: string
}

export const DeleteMethodButton = ({
  projectId,
  methodId,
}: DeleteMethodButtonProps) => {
  const deleteProjectMethodMutation = useDeleteProjectMethodMutation(
    projectId,
    methodId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['projects'] })
          toast.success('Метод успешно удален.')
        },
      },
    },
  )

  const onClick = async () => deleteProjectMethodMutation.mutateAsync({})

  return (
    <TooltipProvider>
      <Tooltip>
        <TooltipTrigger onClick={onClick}>
          <X className='size-4' />
        </TooltipTrigger>
        <TooltipContent>
          <Text>Удалить метод</Text>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  )
}
