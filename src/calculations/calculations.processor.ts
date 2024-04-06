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

	@Process()
	async process(job: Job<{ projectId?: string; methodId?: string }>) {
		this.logger.debug(`Starting calculation: ${JSON.stringify(job.data)}`)
		const projectsToMethods = await this.prisma.projectsMethods.findMany({
			where: {
				OR: [
					{
						projectId: job.data.projectId,
					},
					{
						methodId: job.data.methodId,
					},
				],
			},
		})

		projectsToMethods.map(async ({ methodId, projectId }) => {
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

		this.logger.debug(`Ending calculation ${JSON.stringify(job.data)}`)
	}
}
