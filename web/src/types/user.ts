export type User = {
  id: string
  email: string
  role: Role
}

export type Role = 'Admin' | 'User'
