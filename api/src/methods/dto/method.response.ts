import { ApiProperty } from '@nestjs/swagger'
import type { Methods } from '@prisma/client'
import { CollectorType } from '@prisma/client'

export class MethodResponse implements Methods {
	id: string

	@ApiProperty({ enum: CollectorType, isArray: true })
	collectorTypes: CollectorType[]

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
	property: {
		name: string
		unit: string
	}

	parameters: NestedParameter[]
}
