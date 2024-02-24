import { ApiProperty, OmitType } from '@nestjs/swagger'
import type { Users } from '@prisma/client'
import { Role } from '@prisma/client'

export class UserEntity implements Users {
	id: string

	email: string

	passwordHash: string

	@ApiProperty({ name: 'role', enum: Role, default: Role.User })
	role: Role
}

export class UserResponse extends OmitType(UserEntity, ['passwordHash']) {}
