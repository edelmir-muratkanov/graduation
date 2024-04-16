import {
	Body,
	Controller,
	Get,
	HttpCode,
	HttpStatus,
	Post,
	Req,
	Res,
} from '@nestjs/common'
import { ApiCreatedResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { Users } from '@prisma/client'
import { FastifyReply, FastifyRequest } from 'fastify'
import { Auth, Session } from 'src/shared/decorators'
import { UserResponse } from 'src/users/dto'

import { AuthRequest } from './dto/auth.request'
import { AuthResponse } from './dto/auth.response'
import { COOKIE } from './auth.constants'
import { AuthService } from './auth.service'

@ApiTags('auth')
@Controller('auth')
export class AuthController {
	constructor(private readonly authService: AuthService) {}

	@Post('register')
	@ApiCreatedResponse({ type: AuthResponse })
	async register(
		@Body() request: AuthRequest,
		@Res({ passthrough: true }) res: FastifyReply,
	) {
		const { tokens, user, maxAges } = await this.authService.register(
			request.email,
			request.password,
		)

		res.setCookie(COOKIE.RefreshToken, tokens.refreshToken, {
			path: '/',
			httpOnly: true,
			maxAge: maxAges.refreshTokenMaxAge,
		})

		res.setCookie(COOKIE.AccessToken, tokens.accessToken, {
			path: '/',
			httpOnly: true,
			maxAge: maxAges.accessTokenMaxAge,
		})

		return { user, token: tokens.accessToken }
	}

	@Post('login')
	@HttpCode(HttpStatus.OK)
	@ApiOkResponse({ type: AuthResponse })
	async login(
		@Body() request: AuthRequest,
		@Res({ passthrough: true }) res: FastifyReply,
	) {
		const { tokens, user, maxAges } = await this.authService.login(
			request.email,
			request.password,
		)

		res.setCookie(COOKIE.RefreshToken, tokens.refreshToken, {
			path: '/',
			httpOnly: true,
			maxAge: maxAges.refreshTokenMaxAge,
		})

		res.setCookie(COOKIE.AccessToken, tokens.accessToken, {
			path: '/',
			httpOnly: true,
			maxAge: maxAges.accessTokenMaxAge,
		})

		return { user, token: tokens.accessToken }
	}

	@Get('profile')
	@Auth()
	@ApiOkResponse({ type: UserResponse })
	async getProfile(@Session() session: Users) {
		return {
			id: session.id,
			email: session.email,
			role: session.role,
		}
	}

	@Post('refresh')
	@ApiOkResponse({ type: AuthResponse })
	async refreshTokens(
		@Req() req: FastifyRequest,
		@Res({ passthrough: true }) res: FastifyReply,
	) {
		const oldRefreshToken = req.cookies[COOKIE.RefreshToken]

		const { tokens, maxAges } =
			await this.authService.refreshTokens(oldRefreshToken)

		res.setCookie(COOKIE.RefreshToken, tokens.refreshToken, {
			httpOnly: true,
			maxAge: maxAges.refreshTokenMaxAge,
			path: '/',
		})

		res.setCookie(COOKIE.AccessToken, tokens.accessToken, {
			httpOnly: true,
			maxAge: maxAges.accessTokenMaxAge,
			path: '/',
		})

		return { token: tokens.accessToken }
	}

	@Auth()
	@Post('logout')
	async logout(@Res({ passthrough: true }) res: FastifyReply) {
		const maxAges = this.authService.getTokensExpiration()

		res.clearCookie(COOKIE.AccessToken, {
			maxAge: maxAges.accessTokenMaxAge,
			httpOnly: true,
		})
		res.clearCookie(COOKIE.RefreshToken, {
			maxAge: maxAges.refreshTokenMaxAge,
			httpOnly: true,
		})
	}
}
