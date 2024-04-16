import { i18nValidationMessage } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'

export const validationMessage = i18nValidationMessage<I18nTranslations>
