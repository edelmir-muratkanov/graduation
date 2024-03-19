import { useProfile } from '@/lib/contexts'

import { Text } from '../ui'

import { LoginButton } from './login-button'
import { LogoutButton } from './logout-button'
import { Nav } from './nav'

export const Header = () => {
  const { user } = useProfile()
  return (
    <header className='sticky top-0 z-50 w-full border-b border-border/40 bg-background/95 backdrop:blur supports-[backdrop-filter]:bg-background/60'>
      <div className='container flex h-14 max-w-screen-2xl items-center'>
        <Nav />
        <div className='flex flex-1 items-center justify-between space-x-2 md:justify-end'>
          {user?.id ? (
            <div className='flex items-center gap-2'>
              <Text className='text-sm text-nowrap'>
                {user.email} | {user.role.toUpperCase()}
              </Text>

              <LogoutButton />
            </div>
          ) : (
            <LoginButton />
          )}
        </div>
      </div>
    </header>
  )
}
