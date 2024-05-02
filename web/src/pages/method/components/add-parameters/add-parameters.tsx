import { X } from 'lucide-react'

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
  ScrollArea,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
  Text,
} from '@/components/ui'

import { ParameterField } from './parameter-field'
import { useAddParameter } from './useAddParameters'

export const AddParameters = () => {
  const { state, functions } = useAddParameter()
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
                    className='grid grid-cols-12 grid-rows-3 px-2 gap-y-0 '
                  >
                    <div className='flex justify-between items-center col-span-12 row-span-1'>
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

                    <div className='grid col-span-12 row-span-2'>
                      {state.form.watch(`data.${index}`).first ? (
                        <ParameterField index={index} type='first' />
                      ) : (
                        <Button
                          type='button'
                          variant='secondary'
                          className='mt-1 h-[70px] col-span-12 row-span-1'
                          onClick={() => functions.setParameter(index, 'first')}
                        >
                          Добавить x1
                        </Button>
                      )}

                      {state.form.watch(`data.${index}`).second ? (
                        <ParameterField index={index} type='second' />
                      ) : (
                        <Button
                          type='button'
                          variant='secondary'
                          className='h-[70px] mt-1 col-span-12 row-span-1'
                          onClick={() =>
                            functions.setParameter(index, 'second')
                          }
                        >
                          Добавить x2
                        </Button>
                      )}
                      <FormMessage>
                        {state.formErrors.data &&
                          state.formErrors.data[index]?.message}
                      </FormMessage>
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
