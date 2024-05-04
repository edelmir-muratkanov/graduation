import { X } from 'lucide-react'
import { toast } from 'sonner'

import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
  Text,
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from '@/components/ui'
import { useDeletePropertyMutation } from '@/lib/api/properties/delete-property/hook'
import { cn } from '@/lib/cn'
import { queryClient } from '@/lib/contexts'

interface DeletePropertyProps {
  propertyId: string
  classname?: string
}

export const DeleteProperty = ({
  propertyId,
  classname,
}: DeletePropertyProps) => {
  const deleteMethodMutation = useDeletePropertyMutation(propertyId, {
    options: {
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: ['properties'] })
        toast.success('Свойство успешно удалено.')
      },
    },
  })

  const onClick = async () => deleteMethodMutation.mutateAsync({})

  return (
    <AlertDialog>
      <TooltipProvider>
        <Tooltip>
          <AlertDialogTrigger asChild>
            <TooltipTrigger asChild>
              <X className='size-4 text-destructive' />
            </TooltipTrigger>
          </AlertDialogTrigger>
          <TooltipContent>
            <Text>Удалить свойство</Text>
          </TooltipContent>
        </Tooltip>
      </TooltipProvider>

      <AlertDialogContent className={cn(classname)}>
        <AlertDialogHeader>
          <AlertDialogTitle>
            Вы уверены что хотите удалить свойство?
          </AlertDialogTitle>
          <AlertDialogDescription>
            Это действие будет невозможно отменить.
          </AlertDialogDescription>
        </AlertDialogHeader>

        <AlertDialogFooter>
          <AlertDialogCancel>Отменить</AlertDialogCancel>
          <AlertDialogAction
            className='bg-destructive text-destructive-foreground shadow-sm hover:bg-destructive/90'
            onClick={onClick}
          >
            Удалить
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
