import { ApiProperty } from '@nestjs/swagger'
import { CollectorType } from '@prisma/client'
import { Transform, Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsEnum,
	IsOptional,
	IsString,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { ParameterDto } from './parameter.dto'

export class UpdateMethodRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	name: string

	@ApiProperty({ enum: CollectorType, isArray: true })
	@IsEnum(CollectorType, { each: true })
	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	collectorTypes: CollectorType[]

	@IsOptional()
	@ValidateNested({ each: true })
	@Type(() => ParameterDto)
	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	data: ParameterDto[]
}
