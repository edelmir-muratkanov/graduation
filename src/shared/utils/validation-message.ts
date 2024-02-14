import { i18nValidationMessage } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'

export const validationMessage = i18nValidationMessage<I18nTranslations>
