import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth, Session } from 'src/shared/decorators'

import { CalculationsResponse } from './dto/calculations.response'
import { CreateProjectRequest } from './dto/create-project.request'
import { GetAllProjectsParams } from './dto/get-all-projects-params.request'
import { ProjectResponse } from './dto/project.response'
import { ProjectsResponse } from './dto/projects.response'
import { CalculationsService } from './calculations.service'
import { ProjectsService } from './projects.service'

@ApiTags('projects')
@Controller('projects')
export class ProjectsController {
	constructor(
		private readonly projectsService: ProjectsService,
		private readonly calculationsService: CalculationsService,
	) {}

	@Auth('User')
	@Post()
	async create(
		@Body() request: CreateProjectRequest,
		@Session('id') userId: string,
	) {
		return this.projectsService.create(
			request.name,
			request.country,
			request.operator,
			request.methodIds,
			request.parameters,
			userId,
		)
	}

	@Get()
	@ApiPaginatedResponse(ProjectsResponse)
	async getAll(
		@Query()
		query: GetAllProjectsParams,
	) {
		return this.projectsService.getAll(
			query.userId,
			query.limit,
			query.offset,
			query.lastCursorId,
		)
	}

	@ApiOkResponse({ type: ProjectResponse })
	@Get(':id')
	async getById(@Param('id') id: string) {
		return this.projectsService.getById(id)
	}

	@ApiOkResponse({ type: CalculationsResponse, isArray: true })
	@Get(':id/calculations')
	async getCalculations(@Param('id') id: string) {
		return this.calculationsService.calculate(id)
	}
}
