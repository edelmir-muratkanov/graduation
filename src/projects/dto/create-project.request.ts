import { Type } from 'class-transformer'
import {
	ArrayNotEmpty,
	IsArray,
	IsNotEmpty,
	IsNumber,
	IsPositive,
	IsString,
	ValidateNested,
} from 'class-validator'
import { validationMessage } from 'src/shared/utils'

import type {
	CreateProjectMethodId,
	CreateProjectParameters,
} from '../projects.interface'

export class CreateProjectMethodIdRequest implements CreateProjectMethodId {
	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	methodId: string
}

export class CreateProjectParameterRequest implements CreateProjectParameters {
	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	propertyId: string

	@IsNumber(
		{ allowInfinity: false, allowNaN: false },
		{ message: validationMessage('validation.IsNumber') },
	)
	@IsPositive({ message: validationMessage('validation.IsPositive') })
	value: number
}

export class CreateProjectRequest {
	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	name: string

	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	country: string

	@IsString({ message: validationMessage('validation.IsString') })
	@IsNotEmpty({ message: validationMessage('validation.NotEmpty') })
	operator: string

	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => CreateProjectMethodIdRequest)
	methodIds: CreateProjectMethodIdRequest[]

	@IsArray({ message: validationMessage('validation.IsArray') })
	@ArrayNotEmpty({ message: validationMessage('validation.NotEmpty') })
	@ValidateNested()
	@Type(() => CreateProjectParameterRequest)
	parameters: CreateProjectParameterRequest[]
}
