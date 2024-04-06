import { Cache, CACHE_MANAGER } from '@nestjs/cache-manager'
import {
	Body,
	Controller,
	Delete,
	Get,
	HttpCode,
	HttpStatus,
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
	ApiTags,
} from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth } from 'src/shared/decorators'
import { PaginationParamsRequest } from 'src/shared/pagination'

import { CreatePropertyRequest } from './dto/create-property.request'
import { PropertyResponse } from './dto/property.response'
import {
	PROPERTIES_CACHE_KEY,
	PROPERTY_CACHE_KEY,
} from './properties.constants'
import { PropertiesService } from './properties.service'

@ApiTags('properties')
@Controller('properties')
export class PropertiesController {
	private logger = new Logger(PropertiesController.name)

	constructor(
		private readonly propertiesService: PropertiesService,
		@Inject(CACHE_MANAGER) private readonly cacheManager: Cache,
	) {}

	async clearCache() {
		const keys = await this.cacheManager.store.keys()

		keys.map(async key => {
			if (key.startsWith(PROPERTIES_CACHE_KEY)) {
				await this.cacheManager.del(key)
			}
		})
	}

	@Auth('Admin')
	@Post()
	@ApiCreatedResponse({ type: PropertyResponse })
	async create(@Body() request: CreatePropertyRequest) {
		const property = await this.propertiesService.create(
			request.name,
			request.unit,
		)
		await this.clearCache()

		return property
	}

	@Get()
	@ApiPaginatedResponse(PropertyResponse)
	async getAll(
		@Query() { limit, offset, lastCursorId }: PaginationParamsRequest,
	) {
		const key = [PROPERTIES_CACHE_KEY, limit, offset, lastCursorId].join('-')
		const cached = await this.cacheManager.get(key)
		if (cached) {
			this.logger.log('GET properties from cache')
			return cached
		}
		const properties = await this.propertiesService.getAll(
			limit,
			offset,
			lastCursorId,
		)

		await this.cacheManager.set(key, properties)

		return properties
	}

	@Auth('Admin')
	@Put(':id')
	@HttpCode(HttpStatus.NO_CONTENT)
	@ApiNoContentResponse()
	async update(
		@Param('id') id: string,
		@Body() request: CreatePropertyRequest,
	) {
		await this.propertiesService.update(id, request.name)
		await this.clearCache()
		await this.cacheManager.del(`${PROPERTY_CACHE_KEY}-${id}`)
	}

	@Auth('Admin')
	@Delete(':id')
	@HttpCode(HttpStatus.NO_CONTENT)
	@ApiNoContentResponse()
	async delete(@Param('id') id: string) {
		await this.propertiesService.delete(id)
		await this.clearCache()
		await this.cacheManager.del(`${PROPERTY_CACHE_KEY}-${id}`)
	}
}
