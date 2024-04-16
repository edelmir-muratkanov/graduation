import { useFormContext } from 'react-hook-form'

import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Input,
} from '@/components/ui'

import type { CreateMethodFormSchema } from '../constants'
import { useCreateMethodForm } from '../hooks/useCreateMethodForm'

interface ParameterFieldProps {
  index: number
  type: 'first' | 'second'
}

export const ParameterField = ({ index, type }: ParameterFieldProps) => {
  const { control } = useFormContext<CreateMethodFormSchema>()
  const { state, functions } = useCreateMethodForm()

  return (
    <div className='grid grid-cols-3 col-span-12 gap-2'>
      <FormField
        control={control}
        name={`data.${index}.parameters.${type}.xMin`}
        render={({ field }) => (
          <FormItem>
            <FormLabel className='sr-only'>
              Минимальный x {type === 'first' ? 1 : 2} параметра {index}
            </FormLabel>
            <FormDescription />
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите минимальный x${type === 'first' ? 1 : 2}`}
                {...field}
                value={field.value.toString()}
                onChange={e => field.onChange(functions.convertToNumber(e))}
                disabled={state.loading || field.disabled}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name={`data.${index}.parameters.${type}.x`}
        render={({ field }) => (
          <FormItem>
            <FormLabel className='sr-only'>
              x {type === 'first' ? 1 : 2} параметра {index}
            </FormLabel>
            <FormDescription />
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите x${type === 'first' ? 1 : 2}`}
                {...field}
                value={field.value.toString()}
                onChange={e => field.onChange(functions.convertToNumber(e))}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name={`data.${index}.parameters.${type}.xMax`}
        render={({ field }) => (
          <FormItem>
            <FormLabel className='sr-only'>
              Максимальный x {type === 'first' ? 1 : 2} параметра {index}
            </FormLabel>
            <FormDescription />
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите максимальный x${type === 'first' ? 1 : 2}`}
                {...field}
                value={field.value}
                onChange={e => field.onChange(functions.convertToNumber(e))}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}
