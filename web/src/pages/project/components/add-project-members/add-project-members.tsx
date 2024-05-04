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
  MultipleSelector,
  Text,
} from '@/components/ui'

import { useAddProjectMembers } from './useAddProjectMembers'

export const AddProjectMembers = () => {
  const { state, functions } = useAddProjectMembers()

  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button size='lg'>Добавить участников</Button>
      </DialogTrigger>

      <DialogContent>
        <DialogHeader>
          <DialogTitle>Добавить участников</DialogTitle>
          <DialogDescription>
            <Text>Выберите участников которые будут в проекте</Text>
            <br />
            <Text>Нажмите кнопку чтобы сохранить данные</Text>
          </DialogDescription>
        </DialogHeader>

        <Form {...state.form}>
          <form
            className='space-y-10'
            onSubmit={e => {
              e.preventDefault()
              functions.onSubmit()
            }}
          >
            <FormField
              control={state.form.control}
              name='data'
              render={({ field }) => (
                <FormItem>
                  <FormLabel />
                  <FormDescription />
                  <FormControl>
                    <MultipleSelector
                      className='h-9'
                      defaultOptions={state.users.map(user => ({
                        label: user.email,
                        value: user.id,
                      }))}
                      placeholder='Выберите пользователей'
                      onChange={options =>
                        field.onChange(options.map(option => option.value))
                      }
                      hidePlaceholderWhenSelected
                      emptyIndicator={<div>Ничего не найдено</div>}
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
