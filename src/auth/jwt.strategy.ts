import { Injectable, UnauthorizedException } from '@nestjs/common'
import { PassportStrategy } from '@nestjs/passport'
import { I18nContext, I18nService } from 'nestjs-i18n'
import { ExtractJwt, Strategy } from 'passport-jwt'
import type { I18nTranslations } from 'src/shared/generated'

import type { JwtDto } from './dto/jwt.dto'
import { AuthService } from './auth.service'

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {
	constructor(
		private readonly authService: AuthService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {
		super({
			jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
			secretOrKey: process.env.ACCESS_TOKEN_SECRET,
			ignoreExpiration: false,
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
