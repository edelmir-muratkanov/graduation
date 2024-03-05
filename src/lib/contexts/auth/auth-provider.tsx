import { type FC, type ReactNode, useMemo, useState } from 'react'

import type { Tokens, User } from '@/lib/interfaces'

import { AuthContext } from './auth-context'

export interface AuthProviderProps {
  defaultUser?: User
  defaultTokens?: Tokens
  children: ReactNode
}

export const AuthProvider: FC<AuthProviderProps> = ({
  children,
  defaultTokens,
  defaultUser,
}) => {
  const [user, setUser] = useState<User>(defaultUser!)
  const [tokens, setTokens] = useState<Tokens>(defaultTokens!)

  const value = useMemo(
    () => ({
      user,
      setUser,
      tokens,
      setTokens,
    }),
    [user, tokens],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}
