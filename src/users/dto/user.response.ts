import { ApiProperty, OmitType } from '@nestjs/swagger'
import type { User } from '@prisma/client'
import { Role } from '@prisma/client'

export class UserEntity implements User {
	id: string

	email: string

	passwordHash: string

	@ApiProperty({ name: 'role', enum: Role, default: Role.User })
	role: Role
}

export class UserResponse extends OmitType(UserEntity, ['passwordHash']) {}
