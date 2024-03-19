import { Transform } from 'class-transformer'
import { IsOptional, IsString } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

export class GetAllMethodsRequestParams {
	@IsOptional()
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => value?.trim())
	search?: string
}
