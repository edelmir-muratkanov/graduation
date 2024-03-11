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

interface Property {
  id: string
  name: string
}

interface MethodParamter {
  x: number
  xMin: number
  xMax: number
}

interface Method {
  id: string
  name: string
  parameters: {
    propertyId: Pick<Property, 'id'>
    parameters:
      | {
          values: number[]
        }
      | {
          first?: MethodParamter
          second?: MethodParamter
        }
  }[]
}

interface ProjectStatistic {
  _count: {
    methods: number
    users: number
    parameters: number
  }
}

interface MethodStatistic {
  _count: {
    projects: number
  }
}
