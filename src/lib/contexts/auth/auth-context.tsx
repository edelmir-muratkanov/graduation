import { createContext } from 'react'

import type { Tokens, User } from '@/lib/interfaces'

export interface AuthContextProps {
  user?: User
  tokens?: Tokens

  setUser: (user: User) => void
  setTokens: (tokens: Tokens) => void
}

export const AuthContext = createContext<AuthContextProps>({
  user: undefined!,
  tokens: undefined!,
  setUser: () => {},
  setTokens: () => {},
})
