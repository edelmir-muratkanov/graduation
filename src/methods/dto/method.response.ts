export class MethodResponse {
	id: string

	name: string

	parameters: Parameter[]
}

export class Group {
	x: number

	xMin: number

	xMax: number
}

export class NestedParameter {
	values?: string[]

	first?: Group

	second?: Group
}

export class Parameter {
	propertyId: string

	parameters: NestedParameter[]
}
