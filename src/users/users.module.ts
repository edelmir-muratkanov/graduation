import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'
import { PasswordService } from 'src/shared/services'

import { UsersService } from './users.service'

@Module({
	imports: [PrismaModule],
	providers: [UsersService, PasswordService],
	exports: [UsersService],
})
export class UsersModule {}
