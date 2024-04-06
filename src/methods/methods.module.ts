import { BullModule } from '@nestjs/bull'
import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'

import { MethodsController } from './methods.controller'
import { MethodsService } from './methods.service'

@Module({
	imports: [PrismaModule, BullModule.registerQueue({ name: 'calculations' })],
	controllers: [MethodsController],
	providers: [MethodsService],
})
export class MethodsModule {}
