import { Link, useRouter } from '@tanstack/react-router'

import { cn } from '@/lib/cn'

import { buttonVariants } from '../ui/button'

export const LoginButton = () => {
  const { history } = useRouter()

  if (history.location.pathname.includes('auth')) {
    return null
  }

  return (
    <Link
      to='/auth'
      className={cn(buttonVariants({ variant: 'secondary' }))}
      search={{
        stage: 'login',
        redirectUrl: history.location.pathname,
      }}
    >
      Войти
    </Link>
  )
}
