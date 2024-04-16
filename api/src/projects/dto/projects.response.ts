import { ApiProperty } from '@nestjs/swagger'
import type { Projects } from '@prisma/client'
import { CollectorType, ProjectType } from '@prisma/client'

export class ProjectsResponse implements Projects {
	@ApiProperty({ enum: ProjectType })
	type: ProjectType

	@ApiProperty({ enum: CollectorType })
	collectorType: CollectorType

	_count: {
		parameters: number
		methods: number
		users: number
	}

	id: string

	name: string

	country: string

	operator: string
}
