import { getRouteApi } from '@tanstack/react-router'

import { LoginForm } from './LoginForm/login-form'
import { RegisterForm } from './RegisterForm/register-form'

const { useSearch } = getRouteApi('/auth/')
export const AuthPage = () => {
  const { stage } = useSearch()

  return stage === 'login' ? <LoginForm /> : <RegisterForm />
}
