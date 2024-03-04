import { BadRequestException, Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'
import type { Users } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { HashService } from 'src/shared/services'
import { UsersService } from 'src/users/users.service'

@Injectable()
export class AuthService {
	constructor(
		private readonly jwtService: JwtService,
		private readonly hashService: HashService,
		private readonly usersService: UsersService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async register(email: string, password: string) {
		const user = await this.usersService.createUser(email, password)
		const { accessToken, refreshToken } = await this.generateTokens(user)

		await this.usersService.updateRefreshToken(user.id, refreshToken)

		return { accessToken, refreshToken }
	}

	async login(email: string, password: string) {
		const user = await this.usersService.findByEmail(email)
		const passwordValid = await this.hashService.compareData(
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

		const { accessToken, refreshToken } = await this.generateTokens(user)

		await this.usersService.updateRefreshToken(user.id, refreshToken)

		return { accessToken, refreshToken }
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

	async generateTokens(user: Partial<Users>) {
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
