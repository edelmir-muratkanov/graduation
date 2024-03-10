import { getRouteApi } from '@tanstack/react-router'

import { LoginForm } from './LoginForm/login-form'
import { RegisterForm } from './RegisterForm/register-form'

const route = getRouteApi('/auth/')
export const AuthPage = () => {
  const { stage } = route.useSearch()

  return stage === 'login' ? <LoginForm /> : <RegisterForm />
}
