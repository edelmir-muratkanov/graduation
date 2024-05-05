import { RoleTranslates } from '@/lib/constants'
import { useProfile } from '@/lib/contexts'

import { Button } from '../ui/button'
import { Popover, PopoverContent, PopoverTrigger } from '../ui/popover'
import { Separator } from '../ui/separator'
import { Text } from '../ui/typography'

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
            <Popover>
              <PopoverTrigger asChild>
                <Button variant='secondary'>{user.email}</Button>
              </PopoverTrigger>
              <PopoverContent className='flex flex-col w-max'>
                <Text className='text-center'>{RoleTranslates[user.role]}</Text>
                <Separator className='my-2' />
                <LogoutButton />
              </PopoverContent>
            </Popover>
          ) : (
            <LoginButton />
          )}
        </div>
      </div>
    </header>
  )
}
