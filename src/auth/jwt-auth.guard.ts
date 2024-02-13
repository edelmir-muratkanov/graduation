import { UnauthorizedException } from '@nestjs/common'
import { AuthGuard } from '@nestjs/passport'
import { I18nContext } from 'nestjs-i18n'

export class JwtAuthGuard extends AuthGuard('jwt') {
	handleRequest(err: any, user: any) {
		const i18n = I18nContext.current()

		if (err || !user) {
			throw new UnauthorizedException(i18n.t('exceptions.user.Unauthorized'))
		}

		return user
	}
}
