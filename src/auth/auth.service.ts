import { BadRequestException, Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'
import type { User } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PasswordService } from 'src/shared/services'
import { UsersService } from 'src/users/users.service'

@Injectable()
export class AuthService {
	constructor(
		private readonly jwtService: JwtService,
		private readonly passwordService: PasswordService,
		private readonly usersService: UsersService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async register(email: string, password: string) {
		const user = await this.usersService.createUser(email, password)
		const tokens = await this.generateTokens(user)

		return { user, tokens }
	}

	async login(email: string, password: string) {
		const user = await this.usersService.findByEmail(email)
		const passwordValid = await this.passwordService.comparePasswords(
			password,
			user.passwordHash,
		)

		if (!passwordValid) {
			throw new BadRequestException(
				this.i18n.t('exceptions.user.InvalidCredentials', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		return {
			user: {
				id: user.id,
				email: user.email,
				role: user.role,
			},
			tokens: await this.generateTokens(user),
		}
	}

	async validateUser(userId: string) {
		try {
			return await this.usersService.findById(userId)
		} catch (e) {
			return this.i18n.t('exceptions.user.NotFound', {
				lang: I18nContext.current().lang,
			})
		}
	}

	async generateTokens(user: Partial<User>) {
		const [accessToken, refreshToken] = await Promise.all([
			this.jwtService.signAsync(
				{
					id: user.id,
					email: user.email,
					role: user.role,
				},
				{
					secret: process.env.ACCESS_TOKEN_SECRET,
					expiresIn: process.env.ACCESS_TOKEN_EXPIRATION,
				},
			),
			this.jwtService.signAsync(
				{
					id: user.id,
					email: user.email,
					role: user.role,
				},
				{
					secret: process.env.REFRESH_TOKEN_SECRET,
					expiresIn: process.env.REFRESH_TOKEN_EXPIRATION,
				},
			),
		])

		return { accessToken, refreshToken }
	}
}
