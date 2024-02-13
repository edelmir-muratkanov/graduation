import { Body, Controller, Post } from '@nestjs/common'
import { ApiCreatedResponse, ApiTags } from '@nestjs/swagger'
import { UsersService } from 'src/users/users.service'

import { AuthRequest } from './dto/auth.request'
import { AuthResponse } from './dto/auth.response'
import { AuthService } from './auth.service'

@ApiTags('auth')
@Controller('auth')
export class AuthController {
	constructor(
		private readonly authService: AuthService,
		private readonly usersService: UsersService,
	) {}

	@Post('register')
	@ApiCreatedResponse({ type: AuthResponse })
	async register(@Body() request: AuthRequest) {
		const user = await this.usersService.createUser(
			request.email,
			request.password,
		)

		const tokens = await this.authService.generateTokens(user.id)

		return {
			user,
			tokens,
		}
	}
}
