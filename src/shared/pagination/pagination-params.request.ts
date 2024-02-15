import { Transform } from 'class-transformer'
import { IsNumber, IsOptional, IsString, Min } from 'class-validator'

import { validationMessage } from '../utils/validation-message'

export class PaginationParamsRequest {
	@IsOptional()
	@Transform(({ value }) => Number(value))
	@IsNumber(
		{ allowInfinity: false, allowNaN: false },
		{ message: validationMessage('validation.IsNumber') },
	)
	@Min(0, { message: validationMessage('validation.Min') })
	offset?: number

	@IsOptional()
	@Transform(({ value }) => Number(value))
	@IsNumber(
		{ allowInfinity: false, allowNaN: false },
		{ message: validationMessage('validation.IsNumber') },
	)
	@Min(1, { message: validationMessage('validation.Min') })
	limit?: number

	@IsOptional()
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	lastCursorId?: string
}
