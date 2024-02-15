import { Body, Controller, Get, Post, Query } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'
import { ApiPaginatedResponse } from 'src/shared/decorators/api-paginated-response.decorator'
import { Auth } from 'src/shared/decorators/auth.decorator'
import { PaginationParamsRequest } from 'src/shared/pagination/pagination-params.request'

import { CreateMethodRequest } from './dto/create-method.request'
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
}
