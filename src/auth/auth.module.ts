import { Module } from '@nestjs/common'
import { JwtModule } from '@nestjs/jwt'
import { PasswordService } from 'src/shared/services/password.service'
import { UsersModule } from 'src/users/users.module'

import { AuthController } from './auth.controller'
import { AuthService } from './auth.service'
import { JwtStrategy } from './jwt.strategy'

@Module({
	imports: [UsersModule, JwtModule],
	controllers: [AuthController],
	providers: [AuthService, PasswordService, JwtStrategy],
})
export class AuthModule {}
