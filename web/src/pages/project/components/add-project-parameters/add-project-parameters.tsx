import { X } from 'lucide-react'

import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { ScrollArea } from '@/components/ui/scroll-area'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Text } from '@/components/ui/typography'

import { useAddProjectParameters } from './useAddProjectParameters'

export const AddProjectParameters = () => {
  const { state, functions } = useAddProjectParameters()
  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button size='lg'>Добавить параметры</Button>
      </DialogTrigger>

      <DialogContent className='max-w-4xl'>
        <DialogHeader>
          <DialogTitle>Добавить параметры</DialogTitle>
          <DialogDescription>
            <Text>Нажмите кнопку ниже для добавления параметров</Text> <br />
            <Text>Заполните поля и сохраните</Text>
          </DialogDescription>
        </DialogHeader>

        <Form {...state.form}>
          <form
            onSubmit={e => {
              e.preventDefault()
              functions.onSubmit()
            }}
          >
            <ScrollArea className='h-[500px] pb-8'>
              {state.fields.map((field, index) => {
                return (
                  <li
                    key={field.id}
                    className='grid grid-cols-12 grid-rows-1 px-2 gap-y-0 '
                  >
                    <div className='flex justify-between items-center col-span-12 row-span-2 gap-x-2'>
                      <FormField
                        control={state.form.control}
                        name={`data.${index}.propertyId`}
                        render={({ field }) => (
                          <FormItem className='w-full'>
                            <FormLabel className='sr-only'>
                              Идентификатор {index} свойства
                            </FormLabel>
                            <FormDescription />
                            <Select
                              name={`parameters.${index}.propertyId`}
                              onValueChange={field.onChange}
                              value={field.value}
                              disabled={state.loading || field.disabled}
                            >
                              <FormControl>
                                <SelectTrigger className='m-0'>
                                  <SelectValue placeholder='Выберите свойство' />
                                </SelectTrigger>
                              </FormControl>

                              <SelectContent>
                                {state.properties.map(property => (
                                  <SelectItem
                                    key={property.id}
                                    value={property.id}
                                  >
                                    {property.name}
                                  </SelectItem>
                                ))}
                              </SelectContent>
                            </Select>
                            <FormMessage />
                          </FormItem>
                        )}
                      />

                      <FormField
                        control={state.form.control}
                        name={`data.${index}.value`}
                        render={({ field }) => (
                          <FormItem className='w-[250px]'>
                            <FormLabel className='sr-only'>
                              Значение {index} параметра
                            </FormLabel>
                            <FormDescription />
                            <FormControl>
                              <Input
                                type='number'
                                placeholder='Введите значение'
                                {...field}
                                onChange={e =>
                                  field.onChange(Number(e.target.value) || '')
                                }
                              />
                            </FormControl>

                            <FormMessage />
                          </FormItem>
                        )}
                      />
                      <Button
                        type='button'
                        variant='link'
                        size='icon'
                        className='mt-2'
                        onClick={() => functions.removeParameter(index)}
                      >
                        <X className='size-4 my-auto' />
                      </Button>
                    </div>
                  </li>
                )
              })}
            </ScrollArea>

            <DialogFooter>
              <Button
                type='button'
                variant='outline'
                size='lg'
                onClick={() => functions.addParameter()}
              >
                Добавить параметр
              </Button>
              <Button type='submit'>Сохранить</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
