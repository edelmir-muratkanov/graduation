import { IsString } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

export class CreatePropertyRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	name: string
}
