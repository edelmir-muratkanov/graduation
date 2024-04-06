import { ApiProperty } from '@nestjs/swagger'
import type { Methods, Properties } from '@prisma/client'
import { CollectorType } from '@prisma/client'

class Method implements Pick<Methods, 'id' | 'name'> {
	name: string

	id: string
}

class Property implements Pick<Properties, 'name'> {
	name: string
}

export class CalculationsResponse {
	method: Method

	ratio: number

	applicability: string

	items: CalculationsItem[]
}

class CalculationsItem {
	ratio: number

	property: null | Property

	@ApiProperty({ enum: CollectorType })
	collectorType: CollectorType | null
}
