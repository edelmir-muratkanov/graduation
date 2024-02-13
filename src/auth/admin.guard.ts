import {
	type CanActivate,
	type ExecutionContext,
	ForbiddenException,
} from '@nestjs/common'
import { I18nContext } from 'nestjs-i18n'

export class AdminGuard implements CanActivate {
	canActivate(context: ExecutionContext) {
		const req = context.switchToHttp().getRequest()

		if (!req || !req.user || !req.user.isAdmin) {
			const i18n = I18nContext.current()
			throw new ForbiddenException(i18n.t('exceptions.user.Forbidden'))
		}

		return true
	}
}
