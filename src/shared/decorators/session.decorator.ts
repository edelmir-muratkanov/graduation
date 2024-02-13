import type { ExecutionContext } from '@nestjs/common'
import { createParamDecorator, UnauthorizedException } from '@nestjs/common'
import type { User } from '@prisma/client'
import { I18nContext } from 'nestjs-i18n'

export const Session = createParamDecorator(
	(data: keyof Omit<User, 'passwordHash'>, ctx: ExecutionContext) => {
		const req = ctx.switchToHttp().getRequest<{ user: User }>()

		if (!req || !req.user) {
			const i18n = I18nContext.current()
			throw new UnauthorizedException(i18n.t('exceptions.user.Unauthorized'))
		}

		const { user } = req

		return data ? user[data] : user
	},
)
