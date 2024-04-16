import { ApiPropertyOptional } from '@nestjs/swagger'
import { Type } from 'class-transformer'
import { IsDefined, ValidateIf, ValidateNested } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { GroupDto } from './group.dto'

export class ParameterDataDto {
	@ApiPropertyOptional()
	@ValidateIf(o => !o.second || o.first)
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	first?: GroupDto

	@ApiPropertyOptional()
	@ValidateIf(o => !o.first || o.second)
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	second?: GroupDto
}
