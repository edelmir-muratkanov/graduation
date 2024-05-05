import { X } from 'lucide-react'
import { toast } from 'sonner'

import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import { Text } from '@/components/ui/typography'
import { useDeleteMethodParameterMutation } from '@/lib/api'
import { queryClient } from '@/lib/contexts'

interface DeleteParameterButtonProps {
  methodId: string
  parameterId: string
}

export const DeleteParameterButton = ({
  methodId,
  parameterId,
}: DeleteParameterButtonProps) => {
  const deleteMethodParameterMutation = useDeleteMethodParameterMutation(
    methodId,
    parameterId,
    {
      options: {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ['methods'] })
          toast.success('Параметр успешно удален.')
        },
      },
    },
  )

  const onClick = async () => deleteMethodParameterMutation.mutateAsync({})

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
