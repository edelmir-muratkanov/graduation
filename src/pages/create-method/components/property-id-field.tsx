import { useFormContext } from 'react-hook-form'

import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui'

import { useCreateMethodForm } from '../hooks/useCreateMethodForm'

interface PropertyIdFieldProps {
  index: number
}

export const PropertyIdField = ({ index }: PropertyIdFieldProps) => {
  const { state } = useCreateMethodForm()
  const { control } = useFormContext()
  return (
    <FormField
      control={control}
      name={`data.${index}.propertyId`}
      render={({ field }) => (
        <FormItem className='w-full'>
          <FormLabel className='sr-only'>
            Идентификатор {index} свойства
          </FormLabel>
          <FormDescription />
          <Select
            name={`data.${index}.propertyId`}
            onValueChange={field.onChange}
            value={field.value}
            disabled={state.loading || field.disabled}
          >
            <FormControl>
              <SelectTrigger>
                <SelectValue placeholder='Выберите свойство' />
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
  )
}
