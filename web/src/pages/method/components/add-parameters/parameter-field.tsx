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

import type { AddParametersFormSchema } from './constants'
import { useAddParameter } from './useAddParameters'

interface ParameterFieldProps {
  index: number
  type: 'first' | 'second'
}

export const ParameterField = ({ index, type }: ParameterFieldProps) => {
  const { control } = useFormContext<AddParametersFormSchema>()
  const { state } = useAddParameter()

  return (
    <div className='grid grid-cols-3 col-span-12 gap-x-2'>
      <FormField
        control={control}
        name={`data.${index}.${type}.min`}
        render={({ field }) => (
          <FormItem>
            <FormLabel />
            <FormDescription>Мин. x {type === 'first' ? 1 : 2}</FormDescription>
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите минимальный x${type === 'first' ? 1 : 2}`}
                disabled={state.loading || field.disabled}
                {...field}
                onChange={e => field.onChange(+e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name={`data.${index}.${type}.avg`}
        render={({ field }) => (
          <FormItem>
            <FormLabel />
            <FormDescription>x {type === 'first' ? 1 : 2}</FormDescription>
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите x${type === 'first' ? 1 : 2}`}
                {...field}
                onChange={e => field.onChange(+e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name={`data.${index}.${type}.max`}
        render={({ field }) => (
          <FormItem>
            <FormLabel />
            <FormDescription>Макс x {type === 'first' ? 1 : 2}</FormDescription>
            <FormControl>
              <Input
                type='number'
                placeholder={`Введите максимальный x${type === 'first' ? 1 : 2}`}
                {...field}
                onChange={e => field.onChange(+e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}
