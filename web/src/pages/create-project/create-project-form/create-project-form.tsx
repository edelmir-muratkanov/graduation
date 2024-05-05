import { Delete, Plus } from 'lucide-react'

import { Button } from '@/components/ui/button'
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
import { MultipleSelector } from '@/components/ui/multiple-select'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Heading } from '@/components/ui/typography'
import { CollectorTypeTranslates, ProjectTypeTranslates } from '@/lib/constants'

import { useCreateProjectForm } from './useCreateProjectForm'

export const CreateProjectForm = () => {
  const { functions, state } = useCreateProjectForm()

  return (
    <Form {...state.form}>
      <form
        onSubmit={event => {
          event.preventDefault()
          functions.onSumbit()
        }}
        className='space-y-12'
      >
        <div className='space-y-2'>
          <Heading as='h2'>Базовая информация</Heading>
          <FormField
            control={state.form.control}
            name='name'
            render={({ field }) => (
              <FormItem>
                <FormLabel htmlFor='name' className='sr-only'>
                  Название
                </FormLabel>
                <FormDescription />
                <FormControl>
                  <Input
                    id='name'
                    placeholder='Введите название'
                    disabled={field.disabled || state.loading}
                    autoComplete='on'
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={state.form.control}
            name='country'
            render={({ field }) => (
              <FormItem>
                <FormLabel htmlFor='country' className='sr-only'>
                  Страна
                </FormLabel>
                <FormDescription />
                <FormControl>
                  <Input
                    id='country'
                    placeholder='Введите страну'
                    disabled={field.disabled || state.loading}
                    autoComplete='on'
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={state.form.control}
            name='operator'
            render={({ field }) => (
              <FormItem>
                <FormLabel htmlFor='operator' className='sr-only'>
                  Оператор
                </FormLabel>
                <FormDescription />
                <FormControl>
                  <Input
                    id='operator'
                    placeholder='Введите оператора'
                    disabled={field.disabled || state.loading}
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={state.form.control}
            name='type'
            render={({ field }) => (
              <FormItem>
                <FormLabel htmlFor='type' className='sr-only'>
                  Тип проекта
                </FormLabel>
                <FormDescription />
                <Select
                  name={field.name}
                  value={field.value?.toString()}
                  onValueChange={(value: string) => field.onChange(+value)}
                  disabled={field.disabled || state.loading}
                >
                  <FormControl>
                    <SelectTrigger>
                      <SelectValue
                        id={field.name}
                        placeholder='Выберите тип проекта'
                      />
                    </SelectTrigger>
                  </FormControl>

                  <SelectContent>
                    <SelectItem value='0'>
                      {ProjectTypeTranslates[0]}
                    </SelectItem>
                    <SelectItem value='1'>
                      {ProjectTypeTranslates[1]}
                    </SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={state.form.control}
            name='collectorType'
            render={({ field }) => (
              <FormItem>
                <FormLabel htmlFor='collectorType' className='sr-only'>
                  Тип коллектора
                </FormLabel>
                <FormDescription />
                <Select
                  name={field.name}
                  value={field.value?.toString()}
                  onValueChange={(value: string) => field.onChange(+value)}
                  disabled={field.disabled || state.loading}
                >
                  <FormControl>
                    <SelectTrigger>
                      <SelectValue
                        id={field.name}
                        placeholder='Выберите тип коллектора'
                      />
                    </SelectTrigger>
                  </FormControl>

                  <SelectContent>
                    <SelectItem value='0'>
                      {CollectorTypeTranslates[0]}
                    </SelectItem>
                    <SelectItem value='1'>
                      {CollectorTypeTranslates[1]}
                    </SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={state.form.control}
            name='methodIds'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='sr-only'>Список методов</FormLabel>
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
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <div className='flex flex-col gap-2'>
          <Heading as='h2'>Список параметров</Heading>
          <FormMessage>
            {state.form.formState.errors.parameters?.root?.message}
          </FormMessage>

          <ul>
            {state.parametersFormArray.fields.map((field, index) => (
              <li
                key={field.id}
                className='flex gap-2 items-center justify-center'
              >
                <FormField
                  control={state.form.control}
                  name={`parameters.${index}.propertyId`}
                  render={({ field }) => (
                    <FormItem {...field} className='grow'>
                      <FormLabel className='sr-only'>
                        Идентификатор {index} параметра
                      </FormLabel>
                      <FormDescription />
                      <Select
                        name={`parameters.${index}.propertyId`}
                        onValueChange={field.onChange}
                      >
                        <FormControl>
                          <SelectTrigger>
                            <SelectValue placeholder='Свойство' />
                          </SelectTrigger>
                        </FormControl>

                        <SelectContent>
                          {state.properties.map(property => (
                            <SelectItem key={property.id} value={property.id}>
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
                  name={`parameters.${index}.value`}
                  render={({ field }) => (
                    <FormItem>
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
                  variant='ghost'
                  size='icon'
                  className='mt-2'
                  onClick={() => state.parametersFormArray.remove(index)}
                >
                  <Delete className='size-5 my-auto' />
                </Button>
              </li>
            ))}
          </ul>

          <Button
            type='button'
            variant='ghost'
            size='icon'
            className='self-center mt-2'
            onClick={() =>
              state.parametersFormArray.append({ propertyId: '', value: 0 })
            }
          >
            <Plus className='size-4' />
          </Button>
        </div>

        <Button className='w-full' size='lg' type='submit'>
          Создать
        </Button>
      </form>
    </Form>
  )
}
