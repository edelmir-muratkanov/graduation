import { UnauthorizedException } from '@nestjs/common'
import { PassportStrategy } from '@nestjs/passport'
import type { I18nService } from 'nestjs-i18n'
import { I18nContext } from 'nestjs-i18n'
import { ExtractJwt, Strategy } from 'passport-jwt'
import type { I18nTranslations } from 'src/shared/generated/i18n'

import type { JwtDto } from './dto/jwt.dto'
import type { AuthService } from './auth.service'

export class JwtStrategy extends PassportStrategy(Strategy) {
	constructor(
		private readonly authService: AuthService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {
		super({
			jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
			secretOrKey: process.env.ACCESS_TOKEN_SECRET,
		})
	}

	async validate(payload: JwtDto) {
		const user = await this.authService.validateUser(payload.id)

		if (!user) {
			throw new UnauthorizedException(
				this.i18n.t('exceptions.user.Unauthorized', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		return user
	}
}
