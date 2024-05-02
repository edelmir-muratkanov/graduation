import { usePostLogoutMutation } from '@/lib/api'
import { STORAGE_KEYS } from '@/lib/constants'
import { useProfile } from '@/lib/contexts'

import { Button } from '../ui'

export const LogoutButton = () => {
  const { setUser } = useProfile()
  const postLogoutMutation = usePostLogoutMutation()

  const handleClick = async () => {
    await postLogoutMutation.mutateAsync({})
    localStorage.removeItem(STORAGE_KEYS.AccessToken)
    setUser(undefined!)
  }

  return (
    <Button onClick={handleClick} size='default' variant='link'>
      Выйти
    </Button>
  )
}
