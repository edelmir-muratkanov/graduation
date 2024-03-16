import type { CollectorType, Methods } from '@prisma/client'

export class MethodsResponse implements Methods {
	collectorType: CollectorType[]

	id: string

	name: string

	_count: {
		projects: number
	}
}
