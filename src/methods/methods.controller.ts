import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiCreatedResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth } from 'src/shared/decorators'
import { PaginationParamsRequest } from 'src/shared/pagination'

import { CreateMethodRequest } from './dto/create-method.request'
import { MethodResponse } from './dto/method.response'
import { GetAllMethodsRequestParams } from './dto/methods.request'
import { MethodsResponse } from './dto/methods.response'
import { MethodsService } from './methods.service'

@ApiTags('methods')
@Controller('methods')
export class MethodsController {
	constructor(private readonly methodService: MethodsService) {}

	@Auth('Admin')
	@Post()
	@ApiCreatedResponse({ type: MethodResponse })
	async create(@Body() request: CreateMethodRequest) {
		return this.methodService.create(
			request.name,
			request.collectorTypes,
			request.data,
		)
	}

	@Get()
	@ApiPaginatedResponse(MethodsResponse)
	async getAll(
		@Query() { limit, offset, lastCursorId }: PaginationParamsRequest,
		@Query() params: GetAllMethodsRequestParams,
	) {
		return this.methodService.getAll(limit, offset, lastCursorId, params.search)
	}

	@Get(':id')
	@ApiOkResponse({ type: MethodResponse })
	async getById(@Param('id') id: string) {
		return this.methodService.getById(id)
	}
}
