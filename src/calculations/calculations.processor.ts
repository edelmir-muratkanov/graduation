import { Process, Processor } from '@nestjs/bull'
import { Logger } from '@nestjs/common'
import { Job } from 'bull'
import { PrismaService } from 'src/shared/prisma'

import { CalculationsService } from './calculations.service'

@Processor('calculations')
export class CalculationsProcessor {
	constructor(
		private readonly prisma: PrismaService,
		private readonly calculationsService: CalculationsService,
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
			await this.calculationsService.calculate(projectId, methodId)
		})

		this.logger.debug(
			`Ending calculation on project created with id: ${job.data.projectId}`,
		)
	}
}
