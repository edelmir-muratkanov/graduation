import { InjectQueue } from '@nestjs/bull'
import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { Queue } from 'bull'
import { ApiPaginatedResponse, Auth, Session } from 'src/shared/decorators'

import { CreateProjectRequest } from './dto/create-project.request'
import { GetAllProjectsParams } from './dto/get-all-projects-params.request'
import { ProjectResponse } from './dto/project.response'
import { ProjectsResponse } from './dto/projects.response'
import { ProjectsService } from './projects.service'

@ApiTags('projects')
@Controller('projects')
export class ProjectsController {
	constructor(
		private readonly projectsService: ProjectsService,
		@InjectQueue('calculations') private readonly calculationsQueue: Queue,
	) {}

	@Auth('User')
	@Post()
	async create(
		@Body() request: CreateProjectRequest,
		@Session('id') userId: string,
	) {
		const project = await this.projectsService.create(
			request.name,
			request.country,
			request.projectType,
			request.operator,
			request.methodIds,
			request.parameters,
			userId,
			request.collectorType,
		)
		await this.calculationsQueue.add('project.created', {
			projectId: project.id,
		})
		return project
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
			query.search,
			query.projectType,
			query.collectorType,
		)
	}

	@ApiOkResponse({ type: ProjectResponse })
	@Get(':id')
	async getById(@Param('id') id: string) {
		return this.projectsService.getById(id)
	}
}
