import { ApiPropertyOptional } from '@nestjs/swagger'
import { Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsDefined,
	IsNumber,
	ValidateIf,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { GroupDto } from './group.dto'

export class ParameterDataDto {
	@ApiPropertyOptional()
	@ValidateIf(o => !o.first && !o.second)
	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@IsNumber(
		{},
		{ each: true, message: validationMessage('validation.IsNumber') },
	)
	values?: number[]

	@ApiPropertyOptional()
	@ValidateIf(o => !o.values && !o.second)
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	first?: GroupDto

	@ApiPropertyOptional()
	@ValidateIf(o => !o.values && !o.first)
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	second?: GroupDto
}
