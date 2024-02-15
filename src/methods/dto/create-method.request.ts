import { Transform, Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsString,
	ValidateNested,
} from 'class-validator'

import { ParameterDto } from './parameter.dto'

export class CreateMethodRequest {
	@IsString()
	@Transform(({ value }) => (value as string).trim())
	name: string

	@ValidateNested({ each: true })
	@Type(() => ParameterDto)
	@IsArray()
	@ArrayNotEmpty()
	data: ParameterDto[]
}
