import { Transform, Type } from 'class-transformer'
import {
	IsDefined,
	IsNotEmpty,
	IsString,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { ParameterDataDto } from './parameter-data.dto'

export class ParameterDto {
	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@Transform(({ value }) => (value as string).trim())
	propertyId: string

	@ValidateNested()
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@Type(() => ParameterDataDto)
	parameters: ParameterDataDto
}
