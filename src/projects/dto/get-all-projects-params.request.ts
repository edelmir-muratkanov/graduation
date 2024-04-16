import { ApiPropertyOptional } from '@nestjs/swagger'
import { CollectorType, ProjectType } from '@prisma/client'
import { Transform } from 'class-transformer'
import { IsEnum, IsOptional, IsString } from 'class-validator'
import { PaginationParamsRequest } from 'src/shared/pagination'
import { validationMessage } from 'src/shared/utils'

export class GetAllProjectsParams extends PaginationParamsRequest {
	@IsOptional()
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	userId?: string

	@IsOptional()
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	search?: string

	@ApiPropertyOptional({ enum: ProjectType })
	@IsOptional()
	@IsEnum(ProjectType)
	projectType?: ProjectType

	@ApiPropertyOptional({ enum: CollectorType })
	@IsOptional()
	@IsEnum(CollectorType)
	collectorType?: CollectorType
}
