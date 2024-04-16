import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'
import { HashService } from 'src/shared/services'

import { UsersService } from './users.service'

@Module({
	imports: [PrismaModule],
	providers: [UsersService, HashService],
	exports: [UsersService],
})
export class UsersModule {}
