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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Text } from '@/components/ui/typography'
import { CollectorTypeTranslates, ProjectTypeTranslates } from '@/lib/constants'

import { useUpdateBaseInfo } from './useUpdateProjectBaseInfo'

export const UpdateProjectBaseInfo = () => {
  const { state, functions } = useUpdateBaseInfo()

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
            className='space-y-4'
          >
            <FormField
              control={state.form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='name'>Название проекта</FormLabel>
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
              name='country'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='country'>Страна проекта</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='country'
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
              name='operator'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor='operator'>Оператор проекта</FormLabel>
                  <FormDescription />
                  <FormControl>
                    <Input
                      id='operator'
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
              name='collectorType'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor=':r9:'>Тип коллектора</FormLabel>
                  <FormDescription />
                  <Select
                    name={field.name}
                    value={field.value?.toString()}
                    onValueChange={(value: string) => field.onChange(+value)}
                    disabled={field.disabled || state.loading}
                  >
                    <FormControl>
                      <SelectTrigger>
                        <SelectValue
                          id={field.name}
                          placeholder='Выберите тип коллектора'
                        />
                      </SelectTrigger>
                    </FormControl>

                    <SelectContent>
                      <SelectItem value='0'>
                        {CollectorTypeTranslates[0]}
                      </SelectItem>
                      <SelectItem value='1'>
                        {CollectorTypeTranslates[1]}
                      </SelectItem>
                    </SelectContent>
                  </Select>
                  <FormMessage />
                </FormItem>
              )}
            />

            <FormField
              control={state.form.control}
              name='type'
              render={({ field }) => (
                <FormItem>
                  <FormLabel htmlFor=':r9:'>Тип проекта</FormLabel>
                  <FormDescription />
                  <Select
                    name={field.name}
                    value={field.value?.toString()}
                    onValueChange={(value: string) => field.onChange(+value)}
                    disabled={field.disabled || state.loading}
                  >
                    <FormControl>
                      <SelectTrigger>
                        <SelectValue
                          id={field.name}
                          placeholder='Выберите тип проекта'
                        />
                      </SelectTrigger>
                    </FormControl>

                    <SelectContent>
                      <SelectItem value='0'>
                        {ProjectTypeTranslates[0]}
                      </SelectItem>
                      <SelectItem value='1'>
                        {ProjectTypeTranslates[1]}
                      </SelectItem>
                    </SelectContent>
                  </Select>
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
