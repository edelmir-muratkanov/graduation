import { InjectQueue } from '@nestjs/bull'
import { Cache, CACHE_MANAGER } from '@nestjs/cache-manager'
import {
	Body,
	Controller,
	Get,
	Inject,
	Logger,
	Param,
	Post,
	Put,
	Query,
} from '@nestjs/common'
import { ApiNoContentResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { Queue } from 'bull'
import { ApiPaginatedResponse, Auth, Session } from 'src/shared/decorators'

import {
	CreateProjectRequest,
	GetAllProjectsParams,
	ProjectResponse,
	ProjectsResponse,
	UpdateProjectRequest,
} from './dto'
import { PROJECT_CACHE_KEY, PROJECTS_CACHE_KEY } from './projects.constants'
import { ProjectsService } from './projects.service'

@ApiTags('projects')
@Controller('projects')
export class ProjectsController {
	constructor(
		private readonly projectsService: ProjectsService,
		@InjectQueue('calculations') private readonly calculationsQueue: Queue,
		@Inject(CACHE_MANAGER) private readonly cacheManager: Cache,
	) {}

	private logger = new Logger(ProjectsController.name)

	async clearCache() {
		const keys = await this.cacheManager.store.keys()

		keys.map(async key => {
			if (key.startsWith(PROJECTS_CACHE_KEY)) {
				await this.cacheManager.del(key)
			}
		})
	}

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
		await this.clearCache()
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
		const key = [
			PROJECTS_CACHE_KEY,
			query.collectorType,
			query.lastCursorId,
			query.limit,
			query.offset,
			query.projectType,
			query.search,
			query.userId,
		].join('-')

		const cached = await this.cacheManager.get(key)
		if (cached) {
			this.logger.log(`Get projects from cache`)
			return cached
		}

		const projects = this.projectsService.getAll(
			query.userId,
			query.limit,
			query.offset,
			query.lastCursorId,
			query.search,
			query.projectType,
			query.collectorType,
		)

		await this.cacheManager.set(key, projects)

		return projects
	}

	@ApiOkResponse({ type: ProjectResponse })
	@Get(':id')
	async getById(@Param('id') id: string) {
		const key = `${PROJECT_CACHE_KEY}-${id}`

		const cached = await this.cacheManager.get(key)
		if (cached) {
			return cached
		}

		const project = await this.projectsService.getById(id)
		await this.cacheManager.set(key, project)
		return project
	}

	@Auth()
	@ApiNoContentResponse()
	@Put(':id')
	async update(
		@Param('id') id: string,
		@Session('id') userId: string,
		@Body() request: UpdateProjectRequest,
	) {
		await this.projectsService.update(
			id,
			userId,
			request.name,
			request.operator,
			request.country,
			request.collectorType,
			request.projectType,
			request.methodIds,
			request.parameters,
		)

		await this.clearCache()
		await this.cacheManager.del(`${PROJECT_CACHE_KEY}-${id}`)
	}
}
