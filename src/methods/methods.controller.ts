import { Body, Controller, Get, Param, Post, Query } from '@nestjs/common'
import { ApiOkResponse, ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse, Auth } from 'src/shared/decorators'
import { PaginationParamsRequest } from 'src/shared/pagination'

import { CreateMethodRequest } from './dto/create-method.request'
import { MethodResponse } from './dto/method.response'
import { MethodsResponse } from './dto/methods.response'
import { MethodsService } from './methods.service'

@ApiTags('methods')
@Controller('methods')
export class MethodsController {
	constructor(private readonly methodService: MethodsService) {}

	@Auth('Admin')
	@Post()
	async create(@Body() request: CreateMethodRequest) {
		await this.methodService.create(request.name, request.data)
	}

	@Get()
	@ApiPaginatedResponse(MethodsResponse)
	async getAll(
		@Query() { limit, offset, lastCursorId }: PaginationParamsRequest,
	) {
		return this.methodService.getAll(limit, offset, lastCursorId)
	}

	@Get(':id')
	@ApiOkResponse({ type: MethodResponse })
	async getById(@Param('id') id: string) {
		return this.methodService.getById(id)
	}
}
