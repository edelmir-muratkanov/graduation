import { useFormContext } from 'react-hook-form'

import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { MultipleSelector } from '@/components/ui/multiple-select'
import { Text } from '@/components/ui/typography'
import { CollectorTypeTranslates } from '@/lib/constants'
import { CollectorType } from '@/types'

import { useCreateMethodForm } from '../hooks/useCreateMethodForm'

export const CollectorTypesField = () => {
  const { state } = useCreateMethodForm()
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
                value={field.value.map((v: string) => ({
                  label:
                    +v === 0
                      ? CollectorTypeTranslates[0]
                      : CollectorTypeTranslates[1],
                  value: v,
                }))}
                defaultOptions={[
                  {
                    label: CollectorTypeTranslates[CollectorType.Terrigen],
                    value: CollectorType.Terrigen.toString(),
                  },
                  {
                    label: CollectorTypeTranslates[CollectorType.Carbonate],
                    value: CollectorType.Carbonate.toString(),
                  },
                ]}
                onChange={options => {
                  field.onChange(options.map(o => +o.value))
                }}
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
