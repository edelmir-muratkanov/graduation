import { Body, Controller, Post } from '@nestjs/common'
import { ApiCreatedResponse, ApiTags } from '@nestjs/swagger'
import { Auth } from 'src/shared/decorators/auth.decorator'

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
}
