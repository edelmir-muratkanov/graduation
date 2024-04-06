import { Cache, CACHE_MANAGER } from '@nestjs/cache-manager'
import { Controller, Get, Inject, Logger, Param } from '@nestjs/common'
import { ApiOkResponse, ApiTags } from '@nestjs/swagger'

import { CalculationsResponse } from './dto/calculations.response'
import {
	METHOD_CALCULATIONS_CACHE_KEY,
	PROJECT_CALCULATIONS_CACHE_KEY,
} from './calculations.constants'
import { CalculationsService } from './calculations.service'

@ApiTags('calculations')
@Controller()
export class CalculationsController {
	constructor(
		private readonly calculationsService: CalculationsService,
		@Inject(CACHE_MANAGER) private cacheManager: Cache,
	) {}

	private logger = new Logger(CalculationsController.name)

	@ApiOkResponse({ type: CalculationsResponse, isArray: true })
	@Get('projects/:projectId/calculations')
	async getByProjectId(@Param('projectId') id: string) {
		const key = `${PROJECT_CALCULATIONS_CACHE_KEY}-${id}`
		const cached = await this.cacheManager.get(key)

		if (cached) {
			this.logger.log('GET Project calculations from cache')
			return cached
		}
		const calculations = await this.calculationsService.getByProject(id)
		await this.cacheManager.set(key, calculations)
		return calculations
	}

	@Get('methods/:methodId/calculations')
	@ApiOkResponse({ type: CalculationsResponse, isArray: true })
	async getByMethodId(@Param('methodId') id: string) {
		const key = `${METHOD_CALCULATIONS_CACHE_KEY}-${id}`
		const cached = await this.cacheManager.get(key)
		if (cached) {
			this.logger.log('GET Method calculations from cache')
			return cached
		}

		const calculations = await this.calculationsService.getByMethod(id)
		await this.cacheManager.set(key, calculations)

		return calculations
	}
}
