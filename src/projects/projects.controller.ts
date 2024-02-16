import { Body, Controller, Get, Post, Query } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth, Session } from 'src/shared/decorators'

import { CreateProjectRequest } from './dto/create-project.request'
import { GetAllProjectsParams } from './dto/get-all-projects-params.request'
import { ProjectsResponse } from './dto/projects.response'
import { ProjectsService } from './projects.service'

@ApiTags('projects')
@Controller('projects')
export class ProjectsController {
	constructor(private readonly projectsService: ProjectsService) {}

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
}
