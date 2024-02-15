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

import { GroupDto } from './group.dto'

export class ParameterDataDto {
	@ApiPropertyOptional()
	@ValidateIf(o => !o.first && !o.second)
	@IsArray()
	@ArrayNotEmpty()
	@IsString({ each: true })
	@Transform(({ value }) => (value as string[]).map(v => v.trim()))
	values?: string[]

	@ApiPropertyOptional()
	@ValidateIf(o => !o.values && !o.second)
	@IsDefined()
	@ValidateNested()
	@Type(() => GroupDto)
	first?: GroupDto

	@ApiPropertyOptional()
	@ValidateIf(o => !o.values && !o.first)
	@IsDefined()
	@ValidateNested()
	@Type(() => GroupDto)
	second?: GroupDto
}
