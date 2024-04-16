import type { Users } from '@prisma/client'

export interface JwtDto extends Pick<Users, 'id' | 'email' | 'role'> {
	/**
	 * Issued at
	 * */
	iat: number
	/**
	 * Expiration time
	 */
	exp: number
}
