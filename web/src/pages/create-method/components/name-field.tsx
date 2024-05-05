import { useFormContext } from 'react-hook-form'

import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'

import { useCreateMethodForm } from '../hooks/useCreateMethodForm'

export const NameField = () => {
  const { state } = useCreateMethodForm()
  const { control } = useFormContext()
  return (
    <FormField
      control={control}
      name='name'
      render={({ field }) => (
        <FormItem>
          <FormLabel htmlFor={field.name} className='sr-only'>
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
  )
}
