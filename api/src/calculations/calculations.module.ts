import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'

import { CalculationsController } from './calculations.controller'
import { CalculationsProcessor } from './calculations.processor'
import { CalculationsService } from './calculations.service'

@Module({
	imports: [PrismaModule],
	controllers: [CalculationsController],
	providers: [CalculationsService, CalculationsProcessor],
})
export class CalculationsModule {}
