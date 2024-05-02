import { useNavigate } from '@tanstack/react-router'
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
  Button,
} from '@/components/ui'
import { useDeleteMethodMutation } from '@/lib/api'
import { cn } from '@/lib/cn'
import { queryClient } from '@/lib/contexts'

interface DeleteMethodProps {
  methodId: string
  classname?: string
}

export const DeleteMethod = ({ methodId, classname }: DeleteMethodProps) => {
  const navigate = useNavigate()
  const deleteMethodMutation = useDeleteMethodMutation(methodId, {
    options: {
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: ['methods'] })
        toast.success('Метод успешно удален.')
        navigate({ to: '/methods' })
      },
    },
  })

  const onClick = async () => deleteMethodMutation.mutateAsync({})

  return (
    <AlertDialog>
      <AlertDialogTrigger asChild>
        <Button size='lg' variant='destructive'>
          Удалить метод
        </Button>
      </AlertDialogTrigger>

      <AlertDialogContent className={cn(classname)}>
        <AlertDialogHeader>
          <AlertDialogTitle>
            Вы уверены что хотите удалить метод?
          </AlertDialogTitle>
          <AlertDialogDescription>
            Это действие будет невозможно отменить.
          </AlertDialogDescription>
        </AlertDialogHeader>

        <AlertDialogFooter>
          <AlertDialogCancel>Отменить</AlertDialogCancel>
          <AlertDialogAction onClick={onClick}>Удалить</AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
