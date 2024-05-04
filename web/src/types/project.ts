import type { CollectorType } from './collectorType'
import type { Method } from './method'
import type { Property } from './property'
import type { User } from './user'

export type Project = {
  id: string
  name: string
  country: string
  operator: string
  type: ProjectType
  ownerId: User['id']
  collectorType: CollectorType
  members: Pick<User, 'id' | 'email'>[]
  methods: Pick<Method, 'id' | 'name'>[]
  parameters: ProjectParameter[]
}

export enum ProjectType {
  Ground = 0,
  Shelf = 1,
}

export type ProjectParameter = {
  id: string
  name: Property['name']
  unit: Property['unit']
  value: number
}
