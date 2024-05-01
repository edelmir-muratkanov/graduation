import type { CollectorType } from './collectorType'
import type { Property } from './property'

export type Method = {
  id: string
  name: string
  collectorTypes: CollectorType[]
  parameters: MethodParameter[]
}

export type MethodParameter = {
  id: string
  propertyName: Property['name']
  propertyUnit: Property['unit']
  first?: MethodParameterGroup
  second?: MethodParameterGroup
}

export type MethodParameterGroup = {
  min: number
  avg: number
  max: number
}
