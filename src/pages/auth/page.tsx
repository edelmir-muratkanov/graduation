import { Route } from '@/routes/auth'

import { LoginForm } from './LoginForm/login-form'
import { RegisterForm } from './RegisterForm/register-form'

export const AuthPage = () => {
  const { stage } = Route.useSearch()

  return stage === 'login' ? <LoginForm /> : <RegisterForm />
}
