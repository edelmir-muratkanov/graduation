import type { Property } from '@prisma/client'

export class PropertyResponse implements Property {
	id: string

	name: string
}
