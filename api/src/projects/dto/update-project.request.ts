import { ApiProperty } from '@nestjs/swagger'
import { CollectorType, ProjectType } from '@prisma/client'
import { Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsEnum,
	IsNotEmpty,
	IsOptional,
	IsString,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { CreateProjectParameterRequest } from './create-project.request'

export class UpdateProjectRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	name: string

	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	country: string

	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	operator: string

	@IsOptional()
	@IsEnum(ProjectType)
	@ApiProperty({ enum: ProjectType })
	projectType: ProjectType

	@IsEnum(CollectorType)
	@ApiProperty({ enum: CollectorType })
	collectorType: CollectorType

	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@IsString({ each: true, message: validationMessage('validation.IsString') })
	methodIds: string[]

	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => CreateProjectParameterRequest)
	parameters: CreateProjectParameterRequest[]
}
