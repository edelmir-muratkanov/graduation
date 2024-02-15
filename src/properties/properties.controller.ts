import {
	Body,
	Controller,
	Get,
	HttpCode,
	HttpStatus,
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
import { ApiPaginatedResponse } from 'src/shared/decorators/api-paginated-response.decorator'
import { Auth } from 'src/shared/decorators/auth.decorator'
import { PaginationParamsRequest } from 'src/shared/pagination/pagination-params.request'

import { CreatePropertyRequest } from './dto/create-property.request'
import { PropertyResponse } from './dto/property.response'
import { PropertiesService } from './properties.service'

@ApiTags('properties')
@Controller('properties')
export class PropertiesController {
	constructor(private readonly propertiesService: PropertiesService) {}

	@Auth('Admin')
	@Post()
	@ApiCreatedResponse({ type: PropertyResponse })
	async create(@Body() request: CreatePropertyRequest) {
		return this.propertiesService.create(request.name)
	}

	@Get()
	@ApiPaginatedResponse(PropertyResponse)
	async getAll(
		@Query() { limit, offset, lastCursorId }: PaginationParamsRequest,
	) {
		return this.propertiesService.getAll(limit, offset, lastCursorId)
	}

	@Auth('Admin')
	@Put(':id')
	@HttpCode(HttpStatus.NO_CONTENT)
	@ApiNoContentResponse()
	async update(
		@Param('id') id: string,
		@Body() request: CreatePropertyRequest,
	) {
		return this.propertiesService.update(id, request.name)
	}
}
