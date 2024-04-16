type Role = 'User' | 'Admin'

interface User {
  id: string
  email: string
  role: Role
}

type CollectorType = 'Terrigen' | 'Carbonate'
type ProjectType = 'Ground' | 'Shelf'

interface Project {
  id: string
  name: string
  country: string
  operator: string
  type: ProjectType
  collectorType: CollectorType
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
  collectorTypes: CollectorType[]
  parameters: {
    propertyId: Property['id']
    parameters: {
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

interface PropertyStatistic {
  _count: {
    methods: number
    projects: number
  }
}
