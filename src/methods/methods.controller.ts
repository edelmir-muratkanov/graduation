import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiCreatedResponse, ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth } from 'src/shared/decorators'

import { CreateMethodRequest } from './dto/create-method.request'
import { GetAllMethodsRequestParams } from './dto/get-all-methods-params.request'
import { MethodResponse } from './dto/method.response'
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
	async getAll(@Query() query: GetAllMethodsRequestParams) {
		return this.methodService.getAll(
			query.limit,
			query.offset,
			query.lastCursorId,
			query.search,
			query.collectorType,
		)
	}

	@Get(':id')
	@ApiOkResponse({ type: MethodResponse })
	async getById(@Param('id') id: string) {
		return this.methodService.getById(id)
	}
}
