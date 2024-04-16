import { Module } from '@nestjs/common'
import { JwtModule } from '@nestjs/jwt'
import { PassportModule } from '@nestjs/passport'
import { HashService } from 'src/shared/services'
import { UsersModule } from 'src/users/users.module'

import { AuthController } from './auth.controller'
import { AuthService } from './auth.service'
import { JwtStrategy } from './jwt.strategy'

@Module({
	imports: [UsersModule, JwtModule, PassportModule],
	controllers: [AuthController],
	providers: [AuthService, HashService, JwtStrategy],
})
export class AuthModule {}
