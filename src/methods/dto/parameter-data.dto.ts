import { ApiPropertyOptional } from '@nestjs/swagger'
import { Type } from 'class-transformer'
import { IsDefined, ValidateNested } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import { GroupDto } from './group.dto'

export class ParameterDataDto {
	@ApiPropertyOptional()
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	first?: GroupDto

	@ApiPropertyOptional()
	@IsDefined({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => GroupDto)
	second?: GroupDto
}
