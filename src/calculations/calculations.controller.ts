import { Controller, Get, Param } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'

import { CalculationsService } from './calculations.service'

@ApiTags('calculations')
@Controller()
export class CalculationsController {
	constructor(private readonly calculationsService: CalculationsService) {}

	@Get('projects/:projectId/calculations')
	async getByProjectId(@Param('projectId') id: string) {
		return this.calculationsService.getByProject(id)
	}

	@Get('methods/:methodId/calculations')
	async getByMethodId(@Param('methodId') id: string) {
		return this.calculationsService.getByMethod(id)
	}
}
