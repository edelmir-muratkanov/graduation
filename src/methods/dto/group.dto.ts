import { IsNumber } from 'class-validator'
import { validationMessage } from 'src/shared/utils'

export class GroupDto {
	@IsNumber({}, { message: validationMessage('validation.IsNumber') })
	x: number

	@IsNumber({}, { message: validationMessage('validation.IsNumber') })
	xMin: number

	@IsNumber({}, { message: validationMessage('validation.IsNumber') })
	xMax: number
}
