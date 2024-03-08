export class ProjectUserResponse {
	id: string

	email: string
}

class ProjectParameterResponse {
	value: number

	property: {
		id: string
		name: string
	}
}
class ProjetcMethodParameterGroup {
	x: number

	xMin: number

	xMax: number
}

export class ProjectMethodParameter {
	propertyId: string

	parameters:
		| {
				first?: ProjetcMethodParameterGroup
				second?: ProjetcMethodParameterGroup
		  }
		| {
				values: number[]
		  }
}

class ProjectMethodResponse {
	id: string

	name: string

	parameters: ProjectMethodParameter[]
}

export class ProjectResponse {
	id: string

	name: string

	country: string

	operator: string

	users: ProjectUserResponse[]

	parameters: ProjectParameterResponse[]

	methods: ProjectMethodResponse[]
}
