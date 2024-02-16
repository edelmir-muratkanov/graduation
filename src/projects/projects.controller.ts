import { Body, Controller, Post } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'
import { Auth, Session } from 'src/shared/decorators'

import { CreateProjectRequest } from './dto/create-project.request'
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
}
