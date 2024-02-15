import { Transform, Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsString,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { ParameterDto } from './parameter.dto'

export class CreateMethodRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	name: string

	@ValidateNested({ each: true })
	@Type(() => ParameterDto)
	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	data: ParameterDto[]
}
