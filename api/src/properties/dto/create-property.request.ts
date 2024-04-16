import { Transform } from 'class-transformer'
import { IsString } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

export class CreatePropertyRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	name: string

	@IsString({ message: validationMessage('validation.IsString') })
	@Transform(({ value }) => (value as string).trim())
	unit: string
}
