import { getRouteApi } from '@tanstack/react-router'

import { LoginForm } from './LoginForm/login-form'
import { RegisterForm } from './RegisterForm/register-form'

const { useSearch } = getRouteApi('/auth/')
export const AuthPage = () => {
  const { stage } = useSearch()

  return (
    <div className='mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]'>
      {stage === 'register' ? <RegisterForm /> : <LoginForm />}
    </div>
  )
}
