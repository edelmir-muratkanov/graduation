import { Cache, CACHE_MANAGER } from '@nestjs/cache-manager'
import {
	Body,
	Controller,
	Delete,
	Get,
	Inject,
	Logger,
	Param,
	Post,
	Put,
	Query,
} from '@nestjs/common'
import {
	ApiCreatedResponse,
	ApiNoContentResponse,
	ApiOkResponse,
	ApiTags,
} from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth } from 'src/shared/decorators'

import { MethodResponse } from './dto/method.response'
import { MethodsResponse } from './dto/methods.response'
import {
	CreateMethodRequest,
	GetAllMethodsRequestParams,
	UpdateMethodRequest,
} from './dto'
import { METHOD_CACHE_KEY, METHODS_CACHE_KEY } from './methods.constants'
import { MethodsService } from './methods.service'

@ApiTags('methods')
@Controller('methods')
export class MethodsController {
	constructor(
		private readonly methodService: MethodsService,
		@Inject(CACHE_MANAGER) private cacheManager: Cache,
	) {}

	private logger = new Logger(MethodsController.name)

	async clearCache() {
		const keys = await this.cacheManager.store.keys()

		keys.map(async key => {
			if (key.startsWith(METHODS_CACHE_KEY)) {
				await this.cacheManager.del(key)
			}
		})
	}

	@Auth('Admin')
	@Post()
	@ApiCreatedResponse({ type: MethodResponse })
	async create(@Body() request: CreateMethodRequest) {
		const method = await this.methodService.create(
			request.name,
			request.collectorTypes,
			request.data,
		)

		await this.clearCache()

		return method
	}

	@Get()
	@ApiPaginatedResponse(MethodsResponse)
	async getAll(@Query() query: GetAllMethodsRequestParams) {
		const key = [
			METHODS_CACHE_KEY,
			query.collectorType,
			query.lastCursorId,
			query.limit,
			query.offset,
			query.search,
		].join('-')
		const cached = await this.cacheManager.get(key)

		if (cached) {
			this.logger.log(`Get methods from cache`)
			return cached
		}

		const methods = await this.methodService.getAll(
			query.limit,
			query.offset,
			query.lastCursorId,
			query.search,
			query.collectorType,
		)

		if (methods.count !== 0) {
			await this.cacheManager.set(key, methods)
		}

		return methods
	}

	@Get(':id')
	@ApiOkResponse({ type: MethodResponse })
	async getById(@Param('id') id: string) {
		const key = `${METHOD_CACHE_KEY}-${id}`
		const cached = await this.cacheManager.get(key)

		if (cached) {
			this.logger.log('Get method from cache')
			return cached
		}

		const method = await this.methodService.getById(id)

		await this.cacheManager.set(key, method)

		return method
	}

	@Auth('Admin')
	@Delete(':id')
	@ApiNoContentResponse()
	async delete(@Param('id') id: string) {
		await this.methodService.delete(id)
		await this.clearCache()
		await this.cacheManager.del(`${METHOD_CACHE_KEY}-${id}`)
	}

	@Auth('Admin')
	@Put(':id')
	@ApiNoContentResponse()
	async update(@Param('id') id: string, @Body() request: UpdateMethodRequest) {
		await this.methodService.update(
			id,
			request.name,
			request.collectorTypes,
			request.data,
		)
		await this.clearCache()
		await this.cacheManager.del(`${METHOD_CACHE_KEY}-${id}`)
	}
}
