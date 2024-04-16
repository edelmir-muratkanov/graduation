import type { ExecutionContext } from '@nestjs/common'
import { createParamDecorator, UnauthorizedException } from '@nestjs/common'
import type { Users } from '@prisma/client'
import { I18nContext } from 'nestjs-i18n'

export const Session = createParamDecorator(
	(data: keyof Omit<Users, 'passwordHash'>, ctx: ExecutionContext) => {
		const req = ctx.switchToHttp().getRequest<{ user: Users }>()

		if (!req || !req.user) {
			const i18n = I18nContext.current()
			throw new UnauthorizedException(i18n.t('exceptions.user.Unauthorized'))
		}

		const { user } = req

		return data ? user[data] : user
	},
)
