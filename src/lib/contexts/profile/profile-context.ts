import { createContext } from 'react'

import type { User } from '@/lib/interfaces'

export interface ProfileContextProps {
  user?: User
  setUser: (user: User) => void
}

export const ProfileContext = createContext<ProfileContextProps>({
  user: undefined,
  setUser: () => {},
})
