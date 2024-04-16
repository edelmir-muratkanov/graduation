import { type FC, type ReactNode, useMemo, useState } from 'react'

import { ProfileContext } from './profile-context'

export interface ProfileProviderProps {
  children: ReactNode
  defaultUser?: User
}

export const ProfileProvider: FC<ProfileProviderProps> = ({
  children,
  defaultUser,
}) => {
  const [user, setUser] = useState(defaultUser)
  const value = useMemo(() => ({ user, setUser }), [user])
  return (
    <ProfileContext.Provider value={value}>{children}</ProfileContext.Provider>
  )
}
