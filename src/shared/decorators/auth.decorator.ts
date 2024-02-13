import { applyDecorators, UseGuards } from '@nestjs/common'
import { ApiBearerAuth } from '@nestjs/swagger'
import type { Role } from '@prisma/client'
import { AdminGuard } from 'src/auth/admin.guard'
import { JwtAuthGuard } from 'src/auth/jwt-auth.guard'

export const Auth = (role: Role = 'User') =>
	applyDecorators(
		ApiBearerAuth(),
		role === 'Admin'
			? UseGuards(JwtAuthGuard, AdminGuard)
			: UseGuards(JwtAuthGuard),
	)
