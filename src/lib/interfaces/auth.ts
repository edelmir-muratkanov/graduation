export interface User {
  id: string
  email: string
  role: 'User' | 'Admin'
}

export interface Tokens {
  accessToken: string
  refreshToken: string
}
