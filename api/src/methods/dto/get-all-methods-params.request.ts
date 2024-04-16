import { ApiPropertyOptional } from '@nestjs/swagger'
import { CollectorType } from '@prisma/client'
import { Transform } from 'class-transformer'
import { IsEnum, IsOptional, IsString } from 'class-validator'
import { PaginationParamsRequest } from 'src/shared/pagination'
import { validationMessage } from 'src/shared/utils'

export class GetAllMethodsRequestParams extends PaginationParamsRequest {
	@IsOptional()
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => value?.trim())
	search?: string

	@ApiPropertyOptional({ enum: CollectorType })
	@IsOptional()
	@IsEnum(CollectorType)
	collectorType?: CollectorType
}
