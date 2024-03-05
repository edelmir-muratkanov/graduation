import { Route } from '@/routes/auth'

import { LoginForm } from './components/LoginForm/login-form'

export const AuthPage = () => {
  const { stage } = Route.useSearch()

  return stage === 'login' ? <LoginForm /> : null
}
