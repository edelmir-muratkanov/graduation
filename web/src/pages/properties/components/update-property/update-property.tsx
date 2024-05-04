import { Pencil } from 'lucide-react'

import {
  Button,
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Input,
  Text,
} from '@/components/ui'

import { useUpdateProperty } from './useUpdateProperty'

interface UpdatePropertyProps {
  propertyId: string
}

export const UpdateProperty = ({ propertyId }: UpdatePropertyProps) => {
  const { functions, state } = useUpdateProperty(propertyId)
  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button variant='ghost' size='icon'>
          <Pencil className='size-4' />
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Обновить свойство</DialogTitle>
          <DialogDescription>
            <Text>Введите данные в поля которые хотите изменить</Text> <br />
            <Text>Затем нажмите кнопку.</Text>
          </DialogDescription>
        </DialogHeader>

        <Form {...state.form}>
          <form
            onSubmit={event => {
              event.preventDefault()
              functions.onSubmit()
            }}
            className='space-y-4'
          >
            <FormField
              control={state.form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='name'>Название метода</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='name'
                      placeholder='Введите название'
                      disabled={field.disabled}
                      value={field.value === null ? '' : field.value}
                      onChange={e => field.onChange(e.target.value)}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <FormField
              control={state.form.control}
              name='unit'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='unit'>Типы коллектора</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='unit'
                      placeholder='Введите единицы измерения'
                      disabled={field.disabled}
                      value={field.value === null ? '' : field.value}
                      onChange={e => field.onChange(e.target.value)}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <DialogFooter>
              <Button type='submit'>Изменить</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
