import { Process, Processor } from '@nestjs/bull'
import { Cache, CACHE_MANAGER } from '@nestjs/cache-manager'
import { Inject, Logger } from '@nestjs/common'
import { Job } from 'bull'
import { PrismaService } from 'src/shared/prisma'

import {
	METHOD_CALCULATIONS_CACHE_KEY,
	PROJECT_CALCULATIONS_CACHE_KEY,
} from './calculations.constants'
import { CalculationsService } from './calculations.service'

@Processor('calculations')
export class CalculationsProcessor {
	constructor(
		private readonly prisma: PrismaService,
		private readonly calculationsService: CalculationsService,
		@Inject(CACHE_MANAGER) private cacheManager: Cache,
	) {}

	private logger = new Logger(CalculationsProcessor.name)

	@Process('project.created')
	async handleProjectCreated(job: Job<{ projectId: string }>) {
		this.logger.debug(
			`Starting calculation on project created with id: ${JSON.stringify(job.data)}`,
		)
		const methods = await this.prisma.projectsMethods.findMany({
			where: {
				projectId: job.data.projectId,
			},
		})

		methods.map(async ({ methodId, projectId }) => {
			const calculationId = await this.calculationsService.calculate(
				projectId,
				methodId,
			)

			if (calculationId) {
				await this.cacheManager.del(
					`${PROJECT_CALCULATIONS_CACHE_KEY}-${projectId}`,
				)
				await this.cacheManager.del(
					`${METHOD_CALCULATIONS_CACHE_KEY}-${methodId}`,
				)
			}
		})

		this.logger.debug(
			`Ending calculation on project created with id: ${job.data.projectId}`,
		)
	}
}
