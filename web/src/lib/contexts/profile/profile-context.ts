import { createContext } from 'react'

export interface ProfileContextProps {
  user?: User
  setUser: (user: User) => void
}

export const ProfileContext = createContext<ProfileContextProps>({
  user: undefined,
  setUser: () => {},
})
