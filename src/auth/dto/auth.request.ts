import { Transform } from 'class-transformer'
import { IsEmail, IsString, MinLength } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

export class AuthRequest {
	@IsEmail({}, { message: validationMessage('validation.IsEmail') })
	@Transform(({ value }) => (value as string).trim())
	email: string

	@IsString({ message: validationMessage('validation.IsString') })
	@MinLength(6, { message: validationMessage('validation.MinLength') })
	@Transform(({ value }) => (value as string).trim())
	password: string
}
