import { IsString } from 'class-validator'
import { validationMessage } from 'src/shared/utils/validation-message'

export class CreatePropertyRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	name: string
}
