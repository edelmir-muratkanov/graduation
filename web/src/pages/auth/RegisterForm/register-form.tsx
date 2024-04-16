import {
  Button,
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
  Input,
  Label,
  PasswordInput,
} from '@/components/ui'

import { useRegisterForm } from './useRegisterForm'

export const RegisterForm = () => {
  const { form, goToLogin, loading, onSubmit } = useRegisterForm()
  return (
    <>
      <div className='flex flex-col space-y-2 text-center'>
        <h1 className='text-2xl font-semibold tracking-tight'>
          Создать новую учетную запись
        </h1>
        <p className='text-sm text-muted-foreground'>
          Введите свой адрес электронной почты и пароль
        </p>
      </div>
      <div>
        <Form {...form}>
          <form
            onSubmit={event => {
              event.preventDefault()
              onSubmit()
            }}
            className='space-y-4'
          >
            <FormField
              control={form.control}
              name='email'
              render={({ field }) => (
                <FormItem>
                  <Label className='sr-only' htmlFor='email'>
                    Адрес электронной почты
                  </Label>
                  <FormControl>
                    <Input
                      id='email'
                      placeholder='Введите адрес электронной почты'
                      autoCapitalize='none'
                      autoCorrect='off'
                      disabled={loading}
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='password'
              render={({ field }) => (
                <FormItem>
                  <Label className='sr-only' htmlFor='password'>
                    Пароль
                  </Label>
                  <FormControl>
                    <PasswordInput
                      id='password'
                      placeholder='Введите пароль'
                      autoComplete='password'
                      autoCapitalize='none'
                      autoCorrect='off'
                      disabled={loading}
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='confirmPassword'
              render={({ field }) => (
                <FormItem>
                  <Label className='sr-only' htmlFor='confirmPassword'>
                    Подтвердите пароль
                  </Label>
                  <FormControl>
                    <PasswordInput
                      id='confirmPassword'
                      placeholder='Подтвердите пароль'
                      autoComplete='confirmPassword'
                      autoCapitalize='none'
                      autoCorrect='off'
                      disabled={loading}
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <Button type='submit' className='w-full' loading={loading}>
              <span>Зарегистрироваться</span>
            </Button>
          </form>
        </Form>

        <div className='flex justify-center'>
          <Button disabled={loading} variant='link' onClick={goToLogin}>
            <span className='bg-background px-2 text-muted-foreground'>
              Уже есть аккаунт
            </span>
          </Button>
        </div>
      </div>
    </>
  )
}
