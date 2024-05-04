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

import { useCreateProperty } from './useCreateProperty'

export const CreateProperty = () => {
  const { functions, state } = useCreateProperty()
  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button variant='secondary'>Создать свойство</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Создать свойство</DialogTitle>
          <DialogDescription>
            <Text>Заполните поля ниже. Затем нажмите кнопку.</Text>
          </DialogDescription>
        </DialogHeader>

        <Form {...state.form}>
          <form
            onSubmit={event => {
              event.preventDefault()
              functions.onSubmit()
            }}
            className='space-y-10'
          >
            <FormField
              control={state.form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='name'>Название свойства</FormLabel>
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
                  <FormLabel htmlFor='unit'>Единицы измерения</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='unit'
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
            <DialogFooter>
              <Button type='submit'>Сохранить</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
