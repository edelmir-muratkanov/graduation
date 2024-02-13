import { IsEmail, IsString, MinLength } from 'class-validator'
import { i18nValidationMessage as m } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'

export class AuthRequest {
	@IsEmail({}, { message: m<I18nTranslations>('validation.IsEmail') })
	email: string

	@IsString({ message: m<I18nTranslations>('validation.IsString') })
	@MinLength(6, { message: m<I18nTranslations>('validation.MinLength') })
	password: string
}
