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

import { useAddMethods } from './useAddMethods'

export const AddProjectMethods = () => {
  const { state, functions } = useAddMethods()

  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button size='lg'>Добавить методы</Button>
      </DialogTrigger>

      <DialogContent className='max-w-4xl'>
        <DialogHeader>
          <DialogTitle>Добавить методы</DialogTitle>
          <DialogDescription>
            <Text>Выберите методы которые будут использоваться в проекте</Text>
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
                      defaultOptions={state.methods.map(method => ({
                        label: method.name,
                        value: method.id,
                      }))}
                      placeholder='Выберите методы'
                      onChange={options =>
                        field.onChange(options.map(option => option.value))
                      }
                      hidePlaceholderWhenSelected
                      emptyIndicator={<div>Ничего не найдено</div>}
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
