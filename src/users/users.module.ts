import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma/prisma.module'
import { PasswordService } from 'src/shared/services/password.service'

import { UsersService } from './users.service'

@Module({
	imports: [PrismaModule],
	providers: [UsersService, PasswordService],
	exports: [UsersService],
})
export class UsersModule {}
