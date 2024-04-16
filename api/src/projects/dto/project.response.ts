import { ApiProperty } from '@nestjs/swagger'
import type { Projects } from '@prisma/client'
import { CollectorType, ProjectType } from '@prisma/client'

class ProjectParameterResponse {
	value: number

	property: {
		id: string
		name: string
		unit: string
	}
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

	userIds: string[]

	parameters: ProjectParameterResponse[]

	methodIds: string[]
}
