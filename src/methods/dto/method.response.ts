import type { CollectorType, Methods } from '@prisma/client'

export class MethodResponse implements Methods {
	id: string

	collectorType: CollectorType[]

	name: string

	parameters: Parameter[]
}

export class Group {
	x: number

	xMin: number

	xMax: number
}

export class NestedParameter {
	first?: Group

	second?: Group
}

export class Parameter {
	propertyId: string

	parameters: NestedParameter[]
}
