import { Delete, Plus } from 'lucide-react'

import {
  Button,
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Heading,
  Input,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui'
import MultipleSelector from '@/components/ui/multiple-select'

import { useCreateProjectForm } from './useCreateProjectForm'

export const CreateProjectForm = () => {
  const { functions, state } = useCreateProjectForm()

  return (
    <div className='mx-auto flex flex-col w-full justify-center space-y-6 sm:w-[650px]'>
      <div className='flex flex-col space-y-2 text-center'>
        <Heading as='h1' className='text-2xl font-semibold tracking-tight'>
          Create new project
        </Heading>
      </div>

      <Form {...state.form}>
        <form
          onSubmit={event => {
            event.preventDefault()
            functions.onSumbit()
          }}
          className='space-y-12'
        >
          <div className='space-y-2'>
            <Heading as='h2'>Base info</Heading>
            <FormField
              control={state.form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='name' className='sr-only'>
                    Name
                  </FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='name'
                      placeholder='Enter name'
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
                    Country
                  </FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='country'
                      placeholder='Enter country'
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
                    Operator
                  </FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='operator'
                      placeholder='Enter operator'
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
              name='methodIds'
              render={({ field }) => (
                <FormItem>
                  <FormLabel className='sr-only'>Methods</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <MultipleSelector
                      {...field}
                      className='h-9'
                      value={
                        field.value?.map(m => ({
                          value: m.methodId,
                          label: state.methods.find(
                            method => method.id === m.methodId,
                          )?.name as string,
                        })) || []
                      }
                      defaultOptions={state.methods.map(m => ({
                        label: m.name,
                        value: m.id,
                      }))}
                      placeholder='Select methods'
                      onChange={options =>
                        field.onChange(
                          options.map(option => ({ methodId: option.value })),
                        )
                      }
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>

          <div className='flex flex-col gap-2'>
            <Heading as='h2'>Parameters</Heading>
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
                          Parameter {index} id
                        </FormLabel>
                        <FormDescription />
                        <Select
                          name={`parameters.${index}.propertyId`}
                          onValueChange={field.onChange}
                        >
                          <FormControl>
                            <SelectTrigger>
                              <SelectValue placeholder='Property' />
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
                          Parameter {index} value
                        </FormLabel>
                        <FormDescription />
                        <FormControl>
                          <Input
                            type='number'
                            placeholder='Enter value'
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
            Create
          </Button>
        </form>
      </Form>
    </div>
  )
}
