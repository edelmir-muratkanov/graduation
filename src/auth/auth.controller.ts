import {
	Body,
	Controller,
	Get,
	HttpCode,
	HttpStatus,
	Post,
	Res,
} from '@nestjs/common'
import { ApiCreatedResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { Users } from '@prisma/client'
import { Response } from 'express'
import ms from 'ms'
import { Auth, Session } from 'src/shared/decorators'
import { UserResponse } from 'src/users/dto'

import { AuthRequest } from './dto/auth.request'
import { AuthResponse } from './dto/auth.response'
import { AuthService } from './auth.service'

@ApiTags('auth')
@Controller('auth')
export class AuthController {
	constructor(private readonly authService: AuthService) {}

	@Post('register')
	@ApiCreatedResponse({ type: AuthResponse })
	async register(
		@Body() request: AuthRequest,
		@Res({ passthrough: true }) res: Response,
	) {
		const { accessToken, refreshToken } = await this.authService.register(
			request.email,
			request.password,
		)
		const maxAge = ms(process.env.ACCESS_TOKEN_EXPIRATION)

		res.cookie('refreshToken', refreshToken, { httpOnly: true, maxAge })

		return { accessToken }
	}

	@Post('login')
	@HttpCode(HttpStatus.OK)
	@ApiOkResponse({ type: AuthResponse })
	async login(
		@Body() request: AuthRequest,
		@Res({ passthrough: true }) res: Response,
	) {
		const { accessToken, refreshToken } = await this.authService.login(
			request.email,
			request.password,
		)

		const maxAge = ms(process.env.ACCESS_TOKEN_EXPIRATION)

		res.cookie('refreshToken', refreshToken, { httpOnly: true, maxAge })

		return { accessToken }
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
}
