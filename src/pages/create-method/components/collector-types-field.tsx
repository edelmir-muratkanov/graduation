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
              Collector types
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
                  { label: 'Terrigen', value: 'Terrigen' },
                  { label: 'Carbonate', value: 'Carbonate' },
                ]}
                onChange={options => field.onChange(options.map(o => o.value))}
                placeholder='Select collector types'
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
