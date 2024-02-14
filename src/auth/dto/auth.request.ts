import { IsEmail, IsString, MinLength } from 'class-validator'
import { validationMessage } from 'src/shared/utils/validation-message'

export class AuthRequest {
	@IsEmail({}, { message: validationMessage('validation.IsEmail') })
	email: string

	@IsString({ message: validationMessage('validation.IsString') })
	@MinLength(6, { message: validationMessage('validation.MinLength') })
	password: string
}
