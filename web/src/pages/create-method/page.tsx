import { X } from 'lucide-react'

import { Button, Form, FormMessage, Heading } from '@/components/ui'

import { CollectorTypesField } from './components/collector-types-field'
import { NameField } from './components/name-field'
import { ParameterField } from './components/parameter-field'
import { PropertyIdField } from './components/property-id-field'
import { useCreateMethodForm } from './hooks/useCreateMethodForm'

export const CreateMethodPage = () => {
  const { functions, state } = useCreateMethodForm()

  return (
    <div className='mx-auto flex flex-col w-full justify-center space-y-6 sm:w-[650px]'>
      <div className='flex flex-col space-y-2 text-center'>
        <Heading as='h1' className='text-2xl font-semibold tracking-tight'>
          Создать новый метод
        </Heading>
      </div>

      <Form {...state.form}>
        <form
          onSubmit={event => {
            event.preventDefault()
            functions.onSubmit()
          }}
          className='space-y-10'
        >
          <div className='space-y-2'>
            <NameField />
            <CollectorTypesField />
          </div>

          <div className='flex flex-col'>
            <Heading>Список параметров</Heading>
            <FormMessage>{state.formErrors.parameters?.message}</FormMessage>
            <ul className='space-y-4'>
              {state.parametersFields.map((field, index) => {
                return (
                  <li key={field.id} className='grid grid-cols-12 grid-rows-3'>
                    <div className='flex justify-between items-center col-span-12 row-span-1'>
                      <PropertyIdField index={index} />
                      <Button
                        type='button'
                        variant='link'
                        size='icon'
                        className='mt-2'
                        onClick={() => functions.removeData(index)}
                      >
                        <X className='size-5 my-auto' />
                      </Button>
                    </div>
                    <div className='grid  col-span-12 row-span-2'>
                      {state.form.watch(`parameters.${index}`).first ? (
                        <ParameterField index={index} type='first' />
                      ) : (
                        <Button
                          type='button'
                          variant='secondary'
                          className='mt-2 col-span-12 row-span-1'
                          onClick={() => functions.setParameter(index, 'first')}
                        >
                          Добавить x1
                        </Button>
                      )}

                      {state.form.watch(`parameters.${index}`).second ? (
                        <ParameterField index={index} type='second' />
                      ) : (
                        <Button
                          type='button'
                          variant='secondary'
                          className='mt-2 col-span-12 row-span-1'
                          onClick={() =>
                            functions.setParameter(index, 'second')
                          }
                        >
                          Добавить x2
                        </Button>
                      )}
                      <FormMessage>
                        {state.formErrors.parameters &&
                          state.formErrors.parameters[index]?.message}
                      </FormMessage>
                    </div>
                  </li>
                )
              })}
            </ul>
          </div>
          <div className='flex justify-between items-center gap-2 *:w-full'>
            <Button
              type='button'
              variant='outline'
              size='lg'
              onClick={() => functions.addData()}
            >
              Добавить параметр
            </Button>
            <Button size='lg' type='submit'>
              Создать
            </Button>
          </div>
        </form>
      </Form>
    </div>
  )
}
