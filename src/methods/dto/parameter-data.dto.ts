import { ApiPropertyOptional } from '@nestjs/swagger'
import { Transform, Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsDefined,
	IsString,
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
	@IsString({ each: true, message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string[]).map(v => v.trim()))
	values?: string[]

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
