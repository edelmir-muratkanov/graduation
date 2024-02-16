import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth, Session } from 'src/shared/decorators'

import { CreateProjectRequest } from './dto/create-project.request'
import { GetAllProjectsParams } from './dto/get-all-projects-params.request'
import type { ProjectMethodParameter } from './dto/project.response'
import { ProjectResponse } from './dto/project.response'
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

	@ApiOkResponse({ type: ProjectResponse })
	@Get(':id')
	async getById(@Param('id') id: string) {
		const data = await this.projectsService.getById(id)

		const res = new ProjectResponse()

		res.id = data.id
		res.country = data.country
		res.name = data.name
		res.operator = data.operator
		res.methods = data.methods.map(m => ({
			id: m.method.id,
			name: m.method.name,
			parameters: m.method.parameters.map(p => p as ProjectMethodParameter),
		}))
		res.parameters = data.parameters
		res.users = data.users.map(u => ({ id: u.user.id, email: u.user.email }))

		return res
	}
}
