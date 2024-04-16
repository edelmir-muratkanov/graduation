import { useFormContext } from 'react-hook-form'

import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  MultipleSelector,
  Text,
} from '@/components/ui'
import { CollectorTypeTranslates } from '@/lib/constants'

import { useCreateMethodForm } from '../hooks/useCreateMethodForm'

export const CollectorTypesField = () => {
  const { state, functions } = useCreateMethodForm()
  const { control } = useFormContext()

  return (
    <FormField
      control={control}
      name='collectorTypes'
      render={({ field, fieldState }) => {
        return (
          <FormItem>
            <FormLabel htmlFor=':r9:' className='sr-only'>
              Типы коллекторов
            </FormLabel>
            <FormDescription />
            <FormControl>
              <MultipleSelector
                disabled={state.loading || field.disabled}
                inputProps={{
                  id: field.name,
                  'aria-invalid': fieldState.invalid,
                }}
                value={field.value?.map((v: CollectorType) =>
                  functions.getMultipleSelectorValue(v),
                )}
                defaultOptions={[
                  {
                    label: CollectorTypeTranslates.Terrigen,
                    value: 'Terrigen',
                  },
                  {
                    label: CollectorTypeTranslates.Carbonate,
                    value: 'Carbonate',
                  },
                ]}
                onChange={options => field.onChange(options.map(o => o.value))}
                placeholder='Выберите типы коллекторов'
                hidePlaceholderWhenSelected
                emptyIndicator={<Text>Nothing to select</Text>}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )
      }}
    />
  )
}
