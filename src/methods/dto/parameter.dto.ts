import { Type } from 'class-transformer'
import {
	IsDefined,
	IsNotEmpty,
	IsString,
	ValidateNested,
} from 'class-validator'

import { ParameterDataDto } from './parameter-data.dto'

export class ParameterDto {
	@IsString()
	@IsNotEmpty()
	propertyId: string

	@ValidateNested()
	@IsDefined()
	@Type(() => ParameterDataDto)
	parameters: ParameterDataDto
}
