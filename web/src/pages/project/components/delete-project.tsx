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
} from '@/components/ui/alert-dialog'
import { Button } from '@/components/ui/button'
import { useDeleteProjectMutation } from '@/lib/api'
import { cn } from '@/lib/cn'
import { queryClient } from '@/lib/contexts'

interface DeleteProjectProps {
  projectId: string
  classname?: string
}

export const DeleteProject = ({ projectId, classname }: DeleteProjectProps) => {
  const navigate = useNavigate()
  const deleteProjectMutation = useDeleteProjectMutation(projectId, {
    options: {
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: ['projects'] })
        toast.success('Проект успешно удален.')
        navigate({ to: '/projects' })
      },
    },
  })

  const onClick = async () => deleteProjectMutation.mutateAsync({})

  return (
    <AlertDialog>
      <AlertDialogTrigger asChild>
        <Button size='lg' variant='destructive'>
          Удалить проект
        </Button>
      </AlertDialogTrigger>

      <AlertDialogContent className={cn(classname)}>
        <AlertDialogHeader>
          <AlertDialogTitle>
            Вы уверены что хотите удалить проект?
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
