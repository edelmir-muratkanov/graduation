import { Body, Controller, HttpCode, HttpStatus, Post } from '@nestjs/common'
import { ApiCreatedResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'

import { AuthRequest } from './dto/auth.request'
import { AuthResponse } from './dto/auth.response'
import { AuthService } from './auth.service'

@ApiTags('auth')
@Controller('auth')
export class AuthController {
	constructor(private readonly authService: AuthService) {}

	@Post('register')
	@ApiCreatedResponse({ type: AuthResponse })
	async register(@Body() request: AuthRequest) {
		return this.authService.register(request.email, request.password)
	}

	@Post('login')
	@HttpCode(HttpStatus.OK)
	@ApiOkResponse({ type: AuthResponse })
	async login(@Body() request: AuthRequest) {
		return this.authService.login(request.email, request.password)
	}
}
