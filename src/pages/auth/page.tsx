import { getRouteApi } from '@tanstack/react-router'

import { LoginForm } from './LoginForm/login-form'
import { RegisterForm } from './RegisterForm/register-form'

export const AuthPage = () => {
  const { useSearch } = getRouteApi('/auth')
  const { stage } = useSearch()

  return stage === 'login' ? <LoginForm /> : <RegisterForm />
}
