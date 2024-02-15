import { Body, Controller, Post } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'
import { Auth } from 'src/shared/decorators/auth.decorator'

import { CreateMethodRequest } from './dto/create-method.request'
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
}
