import { BadRequestException, Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'
import { I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'
import { PasswordService } from 'src/shared/services/password.service'
import { UsersService } from 'src/users/users.service'

@Injectable()
export class AuthService {
	constructor(
		private readonly jwtService: JwtService,
		private readonly passwordService: PasswordService,
		private readonly usersService: UsersService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async login(email: string, password: string) {
		const user = await this.usersService.findByEmail(email)
		const passwordValid = await this.passwordService.comparePasswords(
			password,
			user.passwordHash,
		)

		if (!passwordValid) {
			throw new BadRequestException(
				this.i18n.t('exceptions.user.InvalidPassword'),
			)
		}

		return {
			user: {
				id: user.id,
				email: user.email,
				role: user.role,
			},
			tokens: await this.generateTokens(user.id),
		}
	}

	async generateTokens(userId: string) {
		return {
			accessToken: await this.generateAccessToken(userId),
			refreshToken: await this.generateRefreshToken(userId),
		}
	}

	private async generateAccessToken(userId: string) {
		return this.jwtService.signAsync(
			{
				id: userId,
			},
			{
				secret: process.env.ACCESS_TOKEN_SECRET,
				expiresIn: process.env.ACCESS_TOKEN_EXPIRATION,
			},
		)
	}

	private async generateRefreshToken(userId: string) {
		return this.jwtService.signAsync(
			{
				id: userId,
			},
			{
				secret: process.env.REFRESH_TOKEN_SECRET,
				expiresIn: process.env.REFRESH_TOKEN_EXPIRATION,
			},
		)
	}
}
