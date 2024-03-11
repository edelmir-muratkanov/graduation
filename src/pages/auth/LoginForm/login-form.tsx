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

import { useLoginForm } from './useLoginForm'

export const LoginForm = () => {
  const { form, goToRegister, loading, onSubmit } = useLoginForm()
  return (
    <>
      <div className='flex flex-col space-y-2 text-center'>
        <h1 className='text-2xl font-semibold tracking-tight'>
          Login to your account
        </h1>
        <p className='text-sm text-muted-foreground'>
          Enter your email and password
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
                    Email
                  </Label>
                  <FormControl>
                    <Input
                      id='email'
                      placeholder='Enter email'
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
                    Password
                  </Label>
                  <FormControl>
                    <PasswordInput
                      id='password'
                      placeholder='Enter password'
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

            <Button type='submit' className='w-full' loading={loading}>
              <span>Login</span>
            </Button>
          </form>
        </Form>

        <div className='flex justify-center'>
          <Button disabled={loading} variant='link' onClick={goToRegister}>
            <span className='bg-background px-2 text-muted-foreground'>
              Create new account
            </span>
          </Button>
        </div>
      </div>
    </>
  )
}
