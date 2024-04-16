import { ApiProperty } from '@nestjs/swagger'
import type { Methods } from '@prisma/client'
import { CollectorType } from '@prisma/client'

export class MethodsResponse implements Methods {
	@ApiProperty({ enum: CollectorType, isArray: true })
	collectorTypes: CollectorType[]

	id: string

	name: string

	_count: {
		projects: number
	}
}
