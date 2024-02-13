import { Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

@Injectable()
export class AuthService {
	constructor(private readonly jwtService: JwtService) {}

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
