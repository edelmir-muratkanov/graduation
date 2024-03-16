import { ApiProperty } from '@nestjs/swagger'
import type { Projects } from '@prisma/client'
import { CollectorType, ProjectType } from '@prisma/client'

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

	parameters: {
		first?: ProjetcMethodParameterGroup
		second?: ProjetcMethodParameterGroup
	}
}

class ProjectMethodResponse {
	id: string

	name: string

	@ApiProperty({ enum: CollectorType, isArray: true })
	collectorType: CollectorType[]

	parameters: ProjectMethodParameter[]
}

export class ProjectResponse implements Projects {
	@ApiProperty({ enum: ProjectType })
	type: ProjectType

	@ApiProperty({ enum: CollectorType })
	collectorType: CollectorType

	id: string

	name: string

	country: string

	operator: string

	users: ProjectUserResponse[]

	parameters: ProjectParameterResponse[]

	methods: ProjectMethodResponse[]
}
