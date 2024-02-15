import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'

import { MethodsController } from './methods.controller'
import { MethodsService } from './methods.service'

@Module({
	imports: [PrismaModule],
	controllers: [MethodsController],
	providers: [MethodsService],
})
export class MethodsModule {}
