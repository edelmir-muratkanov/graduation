interface User {
  id: string
  email: string
  role: 'User' | 'Admin'
}

interface Project {
  id: string
  name: string
  country: string
  operator: string
}
