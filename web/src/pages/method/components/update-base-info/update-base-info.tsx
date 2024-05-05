import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
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
import { Text } from '@/components/ui/typography'
import { CollectorTypeTranslates } from '@/lib/constants'
import { CollectorType } from '@/types'

import { useUpdateBaseInfo } from './useUpdateBaseInfo'

export const UpdateBaseInfo = () => {
  const { functions, state } = useUpdateBaseInfo()
  return (
    <Dialog
      open={state.isOpen}
      onOpenChange={open => functions.setIsOpen(open)}
    >
      <DialogTrigger asChild>
        <Button size='lg'>Обновить базовую информацию</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Обновить базовой информации о методе</DialogTitle>
          <DialogDescription>
            <Text>Введите данные в поля которые хотите изменить</Text> <br />
            <Text>Затем нажмите кнопку.</Text>
          </DialogDescription>
        </DialogHeader>

        <Form {...state.form}>
          <form
            onSubmit={event => {
              event.preventDefault()
              functions.onSubmit()
            }}
            className='space-y-10'
          >
            <FormField
              control={state.form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='name'>Название метода</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='name'
                      placeholder='Введите название'
                      disabled={field.disabled}
                      value={field.value === null ? '' : field.value}
                      onChange={e => field.onChange(e.target.value)}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <FormField
              control={state.form.control}
              name='collectorTypes'
              render={({ field, fieldState }) => (
                <FormItem>
                  <FormLabel htmlFor=':r9:'>Типы коллектора</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <MultipleSelector
                      disabled={field.disabled}
                      inputProps={{
                        id: field.name,
                        'aria-invalid': fieldState.invalid,
                      }}
                      defaultOptions={[
                        {
                          label:
                            CollectorTypeTranslates[CollectorType.Terrigen],
                          value: CollectorType.Terrigen.toString(),
                        },
                        {
                          label:
                            CollectorTypeTranslates[CollectorType.Carbonate],
                          value: CollectorType.Carbonate.toString(),
                        },
                      ]}
                      value={field.value?.map(v => ({
                        label:
                          v === 0
                            ? CollectorTypeTranslates[0]
                            : CollectorTypeTranslates[1],
                        value: v.toString(),
                      }))}
                      onChange={options =>
                        field.onChange(options.map(o => +o.value))
                      }
                      placeholder='Выберите типы коллекторов'
                      hidePlaceholderWhenSelected
                      emptyIndicator={<Text>Nothing to select</Text>}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <DialogFooter>
              <Button type='submit'>Изменить</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
