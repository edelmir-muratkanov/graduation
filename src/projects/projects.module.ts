import { Module } from '@nestjs/common'
import { PrismaModule } from 'src/shared/prisma'

import { ProjectsController } from './projects.controller'
import { ProjectsService } from './projects.service'

@Module({
	imports: [PrismaModule],
	controllers: [ProjectsController],
	providers: [ProjectsService],
})
export class ProjectsModule {}
