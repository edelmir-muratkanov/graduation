import { IsNumber } from 'class-validator'

export class GroupDto {
	@IsNumber()
	x: number

	@IsNumber()
	xMin: number

	@IsNumber()
	xMax: number
}
